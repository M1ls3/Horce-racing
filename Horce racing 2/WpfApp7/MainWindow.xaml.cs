using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace WpfApp7
{
    public class Horse
    {
        private Color color;
        private string name;
        private int position;
        private TimeSpan raceTime;
        private double coefficient;
        private double money;
        private double distance;

        public Color Color { get { return color; } set { color = value; } }
        public string Name { get { return name; }  set { name = value; } }
        public int Position { get { return position; } set { position = value; } }
        public TimeSpan RaceTime { get { return raceTime; }  set { raceTime = value; } }
        public double Coefficient { get { return coefficient; }  set { coefficient = value; } }
        public double Distance { get { return distance; } set { distance = value; } }
      
        public double Money { get { return money; }  set {money = value; } }


        public int speed = 0;
        Random rand = new Random();

        public Horse(string name, Color color)
        {
            Name = name;
            Color = color;
            speed = rand.Next(5, 10);
            distance = 0;
            coefficient = 1.25;
            money = 0;
        }
        public void SetMoney(int money)
        {
            this.money = money;
        }
        public void ResetSpeed()
        {
            speed = rand.Next(5, 10);
        }
        public double dx;
        public async Task ChangeAcceleration()
        {
            dx = speed * rand.NextDouble();
            await Task.Delay((int)dx);
        }
    }
    public class RenderService
    {
        private readonly int width = 100;
        private readonly int height = 100;

        public RenderService(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public Tuple<int, int> GetRenderSize()
        {
            return Tuple.Create(width, height);
        }
    }
    public partial class MainWindow : Window
    {
        public double balance = 1000;
        private List<Horse> horses = new List<Horse>();
        private List<Task> tasks = new List<Task>();
        private bool isSimulationRunning = false;
        private bool end = false;
        RenderService renderService = new RenderService(800,600);
        private double backgroundOffset = 5;
        private Dictionary<Color, List<ImageSource>> horseAnimationsCache = new Dictionary<Color, List<ImageSource>>();
        int backgroundPosition = 0;
        BitmapImage mask_image_list = new BitmapImage(new Uri(@"Images\Background\Track.png", UriKind.Relative));
        string[] horNames = new string[] { "Чілл", "Нікіта","Кувейт","Єгор","Том" };
        Color[] horColors = new Color[] { Colors.Red,Colors.Blue, Colors.Green, Colors.Orange, Colors.Purple };

        public MainWindow()
        {
            InitializeComponent();
            BetGame();
            for (int i = 0; i < 5; i++)
            {
                horsesNum_Copy.Items.Add(horNames[i]);
                horses.Add(new Horse(horNames[i], horColors[i]));
            }
            InitializeDataGrid();      
            RenderFrame();
        }
        private void InitializeHorses()
        {
            for (int i = 0; i < int.Parse(horsesNum.Text); i++)
            {
                horsesNum_Copy.Items.Add(horNames[i]);
                horses.Add(new Horse(horNames[i], horColors[i]));
            }
        }
        private void InitializeDataGrid()
        {   
            horsesDataGrid.ItemsSource = horses;
        }
        public void RenderFrame() => image.Source = GetRender();
        private BitmapSource GetRender()
        {
            if (backgroundOffset <= 50)
                backgroundOffset += 0.5;
            var render_size = renderService.GetRenderSize();
            var bitmap = new RenderTargetBitmap(
                render_size.Item1, render_size.Item2,
                50, 100, PixelFormats.Pbgra32);
            var drawingvisual = new DrawingVisual();
            using (DrawingContext dc = drawingvisual.RenderOpen())
            {
                Render(dc);
            }
            bitmap.Render(drawingvisual);
            return bitmap;
        }

        private void MoveBackground(DrawingContext dc)
        {
            if (backgroundOffset <= 50)
                backgroundOffset += 0.5;
            backgroundPosition = backgroundPosition - (int)backgroundOffset;
            for (int i = 0; i < 20; i++)
            {
             var rect1 = new Rect(
             new Point(backgroundPosition + i * 1600, 0),
             new Size(1600, 600));
             dc.DrawImage(mask_image_list, rect1);
            }
           
        }
        private async void Render(DrawingContext dc)
        {
            int d = 0;
            List<List<ImageSource>> horseAnimations = horses.Select(horse => GetHorseAnimation(horse.Color)).ToList();
            if (isSimulationRunning == true)
            {
                MoveBackground(dc);
                UpdateHorsePositions();
            }
            for (int i = 0; i < horses.Count; i++)
            {
                var horse = horses[i];
                if (horse.Distance >= 1350)
                {
                    horse.Distance += horse.dx;
                    d++;
                    if (d >= int.Parse(horsesNum.Text) - 1)
                    {
                        double won = 0;
                        string endLine = "";
                        end = true;
                        for (int j = 0; j < horses.Count; j++)
                        {
                            if (horses[j].Position == 1)
                            {
                                won = horses[j].Money * horses[j].Coefficient;
                                BetGame();
                            }
                            endLine += $"Коняка {horses[j].Name} закінчив гонку на {horses[j].Position} на позиції {horses[j].RaceTime.Hours}:{horses[j].RaceTime.Minutes}:{horses[j].RaceTime.Seconds}\n";
                        }
                        endLine += $"Ви перемогли: {won}";
                        MessageBox.Show(endLine);
                        balance += won;
                        BetGame();
                        break;
                    }
                    continue;
                }

                horse.Distance += horse.dx;
                horse.RaceTime += TimeSpan.FromSeconds(1);
                var animationIndex = (int)(horse.Distance) % 10;
                var imageSource = horseAnimations[i][animationIndex];
                var rect = new Rect(
                        new Point(horse.Distance, 70 * (i + 1)),
                        new Size(imageSource.Width, imageSource.Height));
                dc.DrawImage(imageSource, rect);
            }


            foreach (var horse in horses)
            {
                tasks.Add(horse.ChangeAcceleration());
            }
            await Task.WhenAll (tasks);
        }

        private Dictionary<Color, List<ImageSource>> horseAnimationCache = new Dictionary<Color, List<ImageSource>>();
        public List<ImageSource> GetHorseAnimation(Color color)
        {
            if (horseAnimationCache.ContainsKey(color))
            {
                return horseAnimationCache[color];
            }
            const int count = 12;
            var bitmap_image_list = ReadImageList("Images\\Horses", "WithOutBorder_", ".png", count); 
            var mask_image_list = ReadImageList("Images\\HorsesMask", "mask_", ".png", count);
            var animationList = bitmap_image_list.Select((item, index) => GetImageWithColor(item, mask_image_list[index], color)).ToList();
            horseAnimationCache[color] = animationList;
            return animationList;
        }
        private List<BitmapImage> ReadImageList(string path, string name, string format, int count)
        {
             path = $"{path}\\{name}";
             List<BitmapImage> list = new List<BitmapImage>();
            for (int i = 0; i < count; i++)
            {
                var uri = path + string.Format("{0:0000}", i) + format; var img = new BitmapImage(new Uri(uri, UriKind.RelativeOrAbsolute));
                list.Add(img);
            }
            return list;
        }
        private ImageSource GetImageWithColor(BitmapImage image, BitmapImage mask, Color color)
        {
            WriteableBitmap image_bmp = new WriteableBitmap(image);
            WriteableBitmap mask_bmp = new WriteableBitmap(mask);
            WriteableBitmap output_bmp = BitmapFactory.New(image.PixelWidth, image.PixelHeight);
            output_bmp.ForEach((x, y, c) =>
            {
                return MultiplyColors(image_bmp.GetPixel(x, y), color, mask_bmp.GetPixel(x, y).A);
            });
            return output_bmp;
        }
        private Color MultiplyColors(Color color_1, Color color_2, byte alpha)
        {
            var amount = alpha / 255.0;
            byte r = (byte)(color_2.R * amount + color_1.R * (1 - amount));
            byte g = (byte)(color_2.G * amount + color_1.G * (1 - amount));
            byte b = (byte)(color_2.B * amount + color_1.B * (1 - amount));
            return Color.FromArgb(color_1.A, r, g, b);
        }
        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (isSimulationRunning)
                return;
            end = false;
            isSimulationRunning = true;
            while (end != true)
            {
                RenderFrame();
                await Task.Delay(100);
            }
            Reset();
            isSimulationRunning = false;
        }
        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            Reset();
        }
   
         private void UpdateHorsePositions()
        {
            var sortedHorses = horses.OrderByDescending(horse => horse.Distance).ToList();
            for (int i = 0; i < sortedHorses.Count; i++)
            {
                var horse = sortedHorses[i];
                horse.Position = i + 1;
            }
            Dispatcher.Invoke(() => horsesDataGrid.Items.Refresh());
        }

        private void Reset()
        {
            foreach (var horse in horses)
            {
                horse.Position = 0;
                horse.RaceTime = TimeSpan.Zero;
                horse.Distance = 0;
                horse.Money = 0;
                horse.Coefficient = 1.25;
                horse.ResetSpeed();
            }
            backgroundOffset = 0;
            backgroundPosition = 0;
            isSimulationRunning = false;
            end = true;
            RenderFrame();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            horsesNum_Copy.Items.Clear();
            horses.Clear();
            InitializeHorses();
            RenderFrame();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(Convert.ToInt32(betLab.Content) < 100)
            betLab.Content = Convert.ToInt32(betLab.Content) + 10;
          
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt32(betLab.Content) > 10)
                betLab.Content = Convert.ToInt32(betLab.Content) - 10;
        }

        private void betButt_Click(object sender, RoutedEventArgs e)
        {
            if(balance - Convert.ToInt32(betLab.Content) >= 0 )
            {
                balance -= Convert.ToInt32(betLab.Content);
                foreach (var horse in horses)
                {
                    if(horsesNum_Copy.Text == horse.Name)
                    {
                        horse.Coefficient = horse.Coefficient - Convert.ToDouble(betLab.Content) / 500 ;
                        horse.Money += Convert.ToInt32(betLab.Content);
                        continue;
                    }
                    horse.Coefficient = horse.Coefficient + Convert.ToDouble(betLab.Content) / 500;
                }
                UpdateHorsePositions();
                BetGame();
            }
        }
        private void BetGame()
        {
            amBal.Content = balance;
        }
    }
}

