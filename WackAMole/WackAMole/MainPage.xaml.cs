using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WackAMole
{
    public partial class MainPage : ContentPage
    {
        private Stopwatch watch = new Stopwatch();
        private Random _random = new Random();
        private int _countDown;

        public MainPage()
        {
            InitializeComponent();
            BoxView();
            Timer_Tick();
            MoveMole();
            _countDown = 30;
        }

        private void BoxView()
        {
            //add all the boxviews in the 3x3
            int r = 0, c = 0, maxR = 3;
            BoxView b;
            TapGestureRecognizer t = new TapGestureRecognizer();
            t.Tapped += TapGestureRecognizer_Tapped;

            for (r = 0; r < maxR; r++)
            {
                for (c = 0; c < maxR; c++)
                {
                    b = new BoxView();
                    b.HorizontalOptions = LayoutOptions.Center;
                    b.VerticalOptions = LayoutOptions.Center;
                    b.HeightRequest = 50;
                    b.WidthRequest = 50;
                    b.CornerRadius = 0;
                    //b.Color = Color.Green; - testing color
                    //add the gesture recognizer
                    //add the event handler
                    b.GestureRecognizers.Add(t);
                    //attached property (shared)
                    b.SetValue(Grid.RowProperty, r);
                    b.SetValue(Grid.ColumnProperty, c);
                    GameGrid3.Children.Add(b);
                }// end for C = 0
            }//end for R = 0

        }


        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            //Change the color to red
            //To use the sender, cast it to what you want
            //((BoxView)sender).IsVisible = false;
            //((BoxView)sender).Color = Color.Red;
        }

        private void Btn_Start_Clicked(object sender, EventArgs e)
        {
            watch.Start();
        }

        private void Btn_Layout_Clicked(object sender, EventArgs e)
        {
            //Make grid change between 3x3 and 5x5. 
            switch (Btn_Layout.Text)
            {
                case "5 x 5": //5 x 5 visible, switch to 5 x 5
                    {
                        ResetTheGame();
                        GameGrid3.IsVisible = false;
                        GameGrid5.IsVisible = true;
                        Btn_Layout.Text = "3x3";
                        break;
                    }

                case "3x3": //3x3 visible, switch to 3x3
                    {
                        ResetTheGame();
                        GameGrid3.IsVisible = true;
                        GameGrid5.IsVisible = false;
                        Btn_Layout.Text = "5 x 5";
                        break;
                    }


            } //Switch end (Btn_Layout.Text)

        }

        private void ResetTheGame()
        {
            //Reset the Score
            //Reset the Timer
            Label_Score.Text = "0";

            ImageButton imageButton; // local variable

            //makes the mole re-appear/reset to the grid3
            foreach (var item in GameGrid3.Children)
            {
                if (item.GetType() == typeof(ImageButton))
                {
                    imageButton = (ImageButton)item;
                    imageButton.IsVisible = true;
                }
            }

            //makes the mole re-appear/reset to the grid5
            foreach (var item in GameGrid5.Children)
            {
                if (item.GetType() == typeof(ImageButton))
                {
                    imageButton = (ImageButton)item;
                    imageButton.IsVisible = true;
                }
            }
        }

        private void Timer_Tick()
        {
            Device.StartTimer(TimeSpan.FromMilliseconds(2000), () =>
            {
                Device.BeginInvokeOnMainThread(() => { TimerFunctions(); });
                return true;
            });

        }
        private void TimerFunctions()
        {
            _countDown--;
            Label1.Text = _countDown.ToString();
            MoveMole();
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            //Make game Button Dissapear
            //use event handller for all moles -same functionality 
            //add a score for the user then disppear
            //do score label
            int score = Convert.ToInt32(Label_Score.Text);
            score += 10;
            Label_Score.Text = score.ToString();

            //Sender Object
            ImageButton ib = (ImageButton)sender;
            ib.IsVisible = false;
            MoveMole();
        }

        private void MoveMole()
        {
            int MaxRow, MaxCol = 0;
            ImageButton currentImagebutton;

            //which grid visible
            //which mole to move
            if (GameGrid3.IsVisible == true)
            {
                MaxRow = MaxCol = 3;
                currentImagebutton = mole3;
                //Random Number
                //move the mole, make him visible
            }
            else
            {
                MaxRow = MaxCol = 5;
                currentImagebutton = mole5;
                //Random Number
                //move the mole, make him visible
            }

            //set row and col value based on size
            int r = _random.Next(0, MaxRow);
            int c = _random.Next(0, MaxCol);

            //move based on image
            currentImagebutton.SetValue(Grid.RowProperty, r);
            currentImagebutton.SetValue(Grid.ColumnProperty, c);

            //make the button visible
            currentImagebutton.IsVisible = true;

        }

        private void Reset_Clicked(object sender, EventArgs e)
        {
            ResetTheGame();
        }
    }
}
