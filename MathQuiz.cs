using System;
using System.Drawing;
using System.Windows.Forms;

namespace KolmRakendustWindowsFormSamrjakov
{
    public partial class MathQuiz : Form
    {
        // Main Menu

        Label title;
        Button startGameButton, settingsButton;

        // Settings

        Label difficultyLabel, gameModeLabel, parametersLabel;
        Label currentDifficultyLabel, timeParameterLabel;
        Label minimumParameterLabel, maximumParameterLabel;
        Label currentGamemodeLabel;

        Button easyButton, mediumButton, hardButton, customButton;
        Button standardButton, continuousButton;
        Button backButton;

        NumericUpDown customTime, customNumber;

        // Quiz

        Label timeLabel, timeLeftLabel;
        Label question1Label, question2Label;
        Label question3Label, question4Label;
        Label completedQuestionsLabel;

        NumericUpDown answer1, answer2, answer3, answer4;

        Button mainMenuButton;
        Button checkAnswersButton;

        // Timer

        Timer timer1;

        // Random

        Random randomizer = new Random();

        // Questions Data

        int question1Answer;
        int question2Answer;
        int question3Answer;
        int question4Answer;

        // Default Difficulty

        string difficulty = "Easy";

        int gameTime = 60;
        int minimumNumber = 1;
        int maximumNumber = 10;

        // Default Gamemode

        string gameMode = "Standard";

        int completedQuestions = 0;

        // Default Time

        int timeLeft;

        // Default Theme

        Color Backcolor;
        Color Frontcolor;

        // Constructor

        public MathQuiz(string title, int width, int height, Color Backcolor, Color Frontcolor)
        {
            Text = title;
            Size = new Size(width, height);
            StartPosition = FormStartPosition.CenterScreen;

            // Theme
            this.Backcolor = Backcolor;
            this.Frontcolor = Frontcolor;

            BackColor = Backcolor;
            ForeColor = Frontcolor;

            // Form settings
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MaximizeBox = false;

            // Timer
            timer1 = new Timer();
            timer1.Interval = 1000;
            timer1.Tick += Timer1_Tick;

            // Main menu
            ShowMainMenu();

            // Menu
            MainMenu menu = new MainMenu();

            MenuItem menuSettings = new MenuItem("Settings");

            MenuItem menuDarkTheme =
                new MenuItem("Dark Theme", DarkTheme);

            MenuItem menuBrightTheme =
                new MenuItem("Bright Theme", BrightTheme);

            menuSettings.MenuItems.Add(menuDarkTheme);
            menuSettings.MenuItems.Add(menuBrightTheme);

            menu.MenuItems.Add(menuSettings);

            Menu = menu;
        }

        // Main Menu

        private void ShowMainMenu()
        {
            Controls.Clear();

            title = new Label();
            title.Text = "Math Quiz";
            title.Font = new Font("Arial", 30, FontStyle.Bold);
            title.AutoSize = true;
            title.Location = new Point(215, 70);
            Controls.Add(title);

            startGameButton = new Button();
            startGameButton.Text = "Start Game";
            startGameButton.Font = new Font("Arial", 12, FontStyle.Bold);
            startGameButton.Size = new Size(160, 50);
            startGameButton.Location = new Point(220, 180);
            startGameButton.Click += StartGameButton_Click;
            Controls.Add(startGameButton);

            settingsButton = new Button();
            settingsButton.Text = "Settings";
            settingsButton.Font = new Font("Arial", 12, FontStyle.Bold);
            settingsButton.Size = new Size(160, 50);
            settingsButton.Location = new Point(220, 250);
            settingsButton.Click += SettingsButton_Click;
            Controls.Add(settingsButton);

            ApplyTheme();
        }

        // Settings

        private void ShowSettings()
        {
            Controls.Clear();

            title = new Label();
            title.Text = "Settings";
            title.Font = new Font("Arial", 28, FontStyle.Bold);
            title.AutoSize = true;
            title.Location = new Point(225, 30);
            Controls.Add(title);

            // Difficulty
            difficultyLabel = new Label();
            difficultyLabel.Text = "Difficulty";
            difficultyLabel.Font = new Font("Arial", 14, FontStyle.Bold);
            difficultyLabel.AutoSize = true;
            difficultyLabel.Location = new Point(40, 90);
            Controls.Add(difficultyLabel);

            easyButton = new Button();
            easyButton.Text = "Easy";
            easyButton.Size = new Size(110, 40);
            easyButton.Location = new Point(40, 130);
            easyButton.Click += EasyButton_Click;
            Controls.Add(easyButton);

            mediumButton = new Button();
            mediumButton.Text = "Medium";
            mediumButton.Size = new Size(110, 40);
            mediumButton.Location = new Point(40, 180);
            mediumButton.Click += MediumButton_Click;
            Controls.Add(mediumButton);

            hardButton = new Button();
            hardButton.Text = "Hard";
            hardButton.Size = new Size(110, 40);
            hardButton.Location = new Point(40, 230);
            hardButton.Click += HardButton_Click;
            Controls.Add(hardButton);

            customButton = new Button();
            customButton.Text = "Custom";
            customButton.Size = new Size(110, 40);
            customButton.Location = new Point(40, 280);
            customButton.Click += CustomButton_Click;
            Controls.Add(customButton);

            // Game Mode
            gameModeLabel = new Label();
            gameModeLabel.Text = "Game Mode";
            gameModeLabel.Font = new Font("Arial", 14, FontStyle.Bold);
            gameModeLabel.AutoSize = true;
            gameModeLabel.Location = new Point(205, 90);
            Controls.Add(gameModeLabel);

            standardButton = new Button();
            standardButton.Text = "Standard";
            standardButton.Size = new Size(110, 40);
            standardButton.Location = new Point(205, 130);
            standardButton.Click += StandardButton_Click;
            Controls.Add(standardButton);

            continuousButton = new Button();
            continuousButton.Text = "Continuous";
            continuousButton.Size = new Size(110, 40);
            continuousButton.Location = new Point(205, 180);
            continuousButton.Click += ContinuousButton_Click;
            Controls.Add(continuousButton);

            // Parameters
            parametersLabel = new Label();
            parametersLabel.Text = "Parameters";
            parametersLabel.Font = new Font("Arial", 14, FontStyle.Bold);
            parametersLabel.AutoSize = true;
            parametersLabel.Location = new Point(365, 90);
            Controls.Add(parametersLabel);

            currentDifficultyLabel = new Label();
            currentDifficultyLabel.AutoSize = true;
            currentDifficultyLabel.Location = new Point(365, 135);
            Controls.Add(currentDifficultyLabel);

            currentGamemodeLabel = new Label();
            currentGamemodeLabel.AutoSize = true;
            currentGamemodeLabel.Location = new Point(365, 165);
            Controls.Add(currentGamemodeLabel);

            timeParameterLabel = new Label();
            timeParameterLabel.AutoSize = true;
            timeParameterLabel.Location = new Point(365, 195);
            Controls.Add(timeParameterLabel);

            minimumParameterLabel = new Label();
            minimumParameterLabel.AutoSize = true;
            minimumParameterLabel.Location = new Point(365, 225);
            Controls.Add(minimumParameterLabel);

            maximumParameterLabel = new Label();
            maximumParameterLabel.AutoSize = true;
            maximumParameterLabel.Location = new Point(365, 255);
            Controls.Add(maximumParameterLabel);

            // Custom Time
            customTime = new NumericUpDown();
            customTime.Minimum = 10;
            customTime.Maximum = 300;
            customTime.Value = Math.Min(
                Math.Max(gameTime, customTime.Minimum),
                customTime.Maximum);

            customTime.Size = new Size(100, 30);
            customTime.Location = new Point(365, 285);
            customTime.Visible = false;
            customTime.ValueChanged += CustomTime_ValueChanged;
            Controls.Add(customTime);

            // Custom Maximum Number
            customNumber = new NumericUpDown();
            customNumber.Minimum = 5;
            customNumber.Maximum = 1000;
            customNumber.Value = Math.Min(
                Math.Max(maximumNumber, customNumber.Minimum),
                customNumber.Maximum);

            customNumber.Size = new Size(100, 30);
            customNumber.Location = new Point(365, 320);
            customNumber.Visible = false;
            customNumber.ValueChanged += CustomNumber_ValueChanged;
            Controls.Add(customNumber);

            // Back
            backButton = new Button();
            backButton.Text = "Back";
            backButton.Size = new Size(100, 40);
            backButton.Location = new Point(250, 370);
            backButton.Click += BackButton_Click;
            Controls.Add(backButton);

            UpdateSettings();
            ApplyTheme();
        }

        // Settings Change

        private void UpdateSettings()
        {
            if (currentDifficultyLabel == null)
            {
                return;
            }

            currentDifficultyLabel.Text = "Difficulty: " + difficulty;

            currentGamemodeLabel.Text = "Gamemode: " + gameMode;

            timeParameterLabel.Text = "Time: " + gameTime + " sec";

            minimumParameterLabel.Text = "Minimum: " + minimumNumber;

            maximumParameterLabel.Text = "Maximum: " + maximumNumber;

            if (difficulty == "Custom")
            {
                customTime.Visible = true;
                customNumber.Visible = true;

                customTime.Value = Math.Min(Math.Max(gameTime, (int)customTime.Minimum), (int)customTime.Maximum);

                customNumber.Value = Math.Min(Math.Max(maximumNumber, (int)customNumber.Minimum), (int)customNumber.Maximum);
            }
            else
            {
                customTime.Visible = false;
                customNumber.Visible = false;
            }
        }

        // Custom Settings

        private void CustomTime_ValueChanged(object sender, EventArgs e)
        {
            if (difficulty == "Custom")
            {
                gameTime = (int)customTime.Value;
                UpdateSettings();
            }
        }

        private void CustomNumber_ValueChanged(object sender, EventArgs e)
        {
            if (difficulty == "Custom")
            {
                maximumNumber = (int)customNumber.Value;
                UpdateSettings();
            }
        }

        // Difficulty

        private void SetEasy()
        {
            difficulty = "Easy";
            gameTime = 60;
            minimumNumber = 1;
            maximumNumber = 10;

            UpdateSettings();
        }

        private void SetMedium()
        {
            difficulty = "Medium";
            gameTime = 45;
            minimumNumber = 1;
            maximumNumber = 25;

            UpdateSettings();
        }

        private void SetHard()
        {
            difficulty = "Hard";
            gameTime = 30;
            minimumNumber = 1;
            maximumNumber = 50;

            UpdateSettings();
        }

        private void SetCustom()
        {
            difficulty = "Custom";

            UpdateSettings();
        }

        // Game Mode

        private void SetStandard()
        {
            gameMode = "Standard";
            UpdateSettings();
        }

        private void SetContinuous()
        {
            gameMode = "Continuous";
            UpdateSettings();
        }

        // Buttons Click

        private void StartGameButton_Click(object sender, EventArgs e)
        {
            // Make sure custom values are saved.
            if (difficulty == "Custom")
            {
                gameTime = (int)customTime.Value;
                maximumNumber = (int)customNumber.Value;
            }

            completedQuestions = 0;

            ShowQuiz();
            StartTheQuiz();
        }

        private void MainMenuButton_Click(object sender, EventArgs e)
        {
            timer1.Stop();

            ShowMainMenu();
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            ShowSettings();
        }

        private void EasyButton_Click(object sender, EventArgs e)
        {
            SetEasy();
        }

        private void MediumButton_Click(object sender, EventArgs e)
        {
            SetMedium();
        }

        private void HardButton_Click(object sender, EventArgs e)
        {
            SetHard();
        }

        private void CustomButton_Click(object sender,  EventArgs e)
        {
            SetCustom();
        }

        private void StandardButton_Click(object sender, EventArgs e)
        {
            SetStandard();
        }

        private void ContinuousButton_Click(object sender, EventArgs e)
        {
            SetContinuous();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            ShowMainMenu();
        }

        // Quiz

        private void ShowQuiz()
        {
            Controls.Clear();

            // Time Left
            timeLeftLabel = new Label();
            timeLeftLabel.Text = "Time Left:";
            timeLeftLabel.Font = new Font("Arial", 12, FontStyle.Bold);
            timeLeftLabel.AutoSize = true;
            timeLeftLabel.Location = new Point(ClientSize.Width - 180, 20);
            Controls.Add(timeLeftLabel);

            // Timer
            timeLabel = new Label();
            timeLabel.Text = "";
            timeLabel.Font = new Font("Arial", 12, FontStyle.Bold);
            timeLabel.BorderStyle = BorderStyle.FixedSingle;
            timeLabel.TextAlign = ContentAlignment.MiddleCenter;
            timeLabel.Size = new Size(100, 30);
            timeLabel.Location = new Point(ClientSize.Width - 110, 15);
            Controls.Add(timeLabel);

            // Completed Questions
            completedQuestionsLabel = new Label();
            completedQuestionsLabel.Text = "Questions completed: " + completedQuestions;
            completedQuestionsLabel.Font = new Font("Arial", 12, FontStyle.Bold);
            completedQuestionsLabel.AutoSize = true;
            completedQuestionsLabel.Location = new Point(80, 20);
            Controls.Add(completedQuestionsLabel);

            // Question 1
            question1Label = CreateQuestionLabel(80);
            Controls.Add(question1Label);

            answer1 = CreateAnswerBox(85, 1);
            Controls.Add(answer1);

            // Question 2
            question2Label = CreateQuestionLabel(140);
            Controls.Add(question2Label);

            answer2 = CreateAnswerBox(145, 2);
            Controls.Add(answer2);

            // Question 3
            question3Label = CreateQuestionLabel(200);
            Controls.Add(question3Label);

            answer3 = CreateAnswerBox(205, 3);
            Controls.Add(answer3);

            // Question 4
            question4Label = CreateQuestionLabel(260);
            Controls.Add(question4Label);

            answer4 = CreateAnswerBox(265, 4);
            Controls.Add(answer4);

            // Check Answers
            checkAnswersButton = new Button();
            checkAnswersButton.Text = "Check Answers";
            checkAnswersButton.Font = new Font("Arial", 12, FontStyle.Bold);
            checkAnswersButton.Size = new Size(180, 50);
            checkAnswersButton.Location = new Point(210, 340);
            checkAnswersButton.Click += CheckAnswersButton_Click;
            Controls.Add(checkAnswersButton);

            // Main Menu
            mainMenuButton = new Button();
            mainMenuButton.Text = "Main Menu";
            mainMenuButton.Font = new Font("Arial", 12, FontStyle.Bold);
            mainMenuButton.Size = new Size(180, 40);
            mainMenuButton.Location = new Point(210, 400);
            mainMenuButton.Click += MainMenuButton_Click;
            Controls.Add(mainMenuButton);


            ApplyTheme();
        }

        private Label CreateQuestionLabel(int y)
        {
            Label label = new Label();

            label.Font = new Font("Arial", 18);
            label.Size = new Size(250, 50);
            label.Location = new Point(80, y);
            label.TextAlign = ContentAlignment.MiddleLeft;

            return label;
        }

        private NumericUpDown CreateAnswerBox(
            int y,
            int tabIndex)
        {
            NumericUpDown box = new NumericUpDown();

            box.Font = new Font("Arial", 18);
            box.Size = new Size(100, 40);
            box.Location = new Point(350, y);
            box.TabIndex = tabIndex;

            // Large enough for 1000 × 1000.
            box.Maximum = 1000000;

            box.Enter += Answer_Enter;

            return box;
        }

        // Quiz Start

        private void StartTheQuiz()
        {
            answer1.Value = 0;
            answer2.Value = 0;
            answer3.Value = 0;
            answer4.Value = 0;

            CreateQuestion(question1Label, 1);
            CreateQuestion(question2Label, 2);
            CreateQuestion(question3Label, 3);
            CreateQuestion(question4Label, 4);

            checkAnswersButton.Enabled = true;

            // Only starts / resets the timer when a new game begins.
            // In Continuous mode, the timer does NOT reset when a new set of questions is generated.
            if (!timer1.Enabled)
            {
                timeLeft = gameTime;

                timeLabel.Text = timeLeft + " sec";

                timeLabel.BackColor = Backcolor;

                timeLabel.ForeColor = Frontcolor;

                timer1.Start();
            }
        }


        // Random Question

        private void CreateQuestion(Label questionLabel, int questionNumber)
        {
            int questionType = randomizer.Next(1, 5);

            int number1;
            int number2;
            int answer;

            // Addition
            if (questionType == 1)
            {
                number1 = randomizer.Next(minimumNumber, maximumNumber + 1);

                number2 = randomizer.Next( minimumNumber, maximumNumber + 1);

                answer = number1 + number2;

                questionLabel.Text = number1 + " + " + number2 + " =";

                SetQuestionAnswer(questionNumber, answer);
            }

            // Subtraction
            else if (questionType == 2)
            {
                number1 = randomizer.Next(minimumNumber, maximumNumber + 1);

                number2 = randomizer.Next(minimumNumber, number1 + 1);

                answer = number1 - number2;

                questionLabel.Text = number1 + " - " + number2 + " =";

                SetQuestionAnswer(questionNumber, answer);
            }

            // Multiplication
            else if (questionType == 3)
            {
                number1 = randomizer.Next(minimumNumber, maximumNumber + 1);

                number2 = randomizer.Next(minimumNumber, maximumNumber + 1);

                answer = number1 * number2;

                questionLabel.Text = number1 + " × " + number2 + " =";

                SetQuestionAnswer(questionNumber, answer);
            }

            // Division
            else
            {
                number2 = randomizer.Next(minimumNumber, maximumNumber + 1);

                int temporaryAnswer = randomizer.Next(minimumNumber, maximumNumber + 1);

                number1 = number2 * temporaryAnswer;

                answer = number1 / number2;

                questionLabel.Text = number1 + " ÷ " + number2 + " =";

                SetQuestionAnswer(questionNumber, answer);
            }
        }

        // Answers

        private void SetQuestionAnswer(int questionNumber, int answer)
        {
            if (questionNumber == 1)
            {
                question1Answer = answer;
            }
            else if (questionNumber == 2)
            {
                question2Answer = answer;
            }
            else if (questionNumber == 3)
            {
                question3Answer = answer;
            }
            else if (questionNumber == 4)
            {
                question4Answer = answer;
            }
        }

        // Check Answers Button

        private void CheckAnswersButton_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled)
            {
                return;
            }

            bool correct = true;

            if (answer1.Value != question1Answer)
            {
                correct = false;
            }

            if (answer2.Value != question2Answer)
            {
                correct = false;
            }      

            if (answer3.Value != question3Answer)
            {
                correct = false;
            }

            if (answer4.Value != question4Answer)
            {
                correct = false;
            }

            if (correct)
            {
                if (gameMode == "Continuous")
                {
                    completedQuestions += 4;

                    completedQuestionsLabel.Text = "Questions completed: " + completedQuestions;

                    // Generate the next four questions
                    // and reset the timer.
                    StartTheQuiz();

                    return;
                }

                timer1.Stop();

                MessageBox.Show("All answers are correct!", "Math Quiz");

                checkAnswersButton.Enabled = false;

                return;
            }

            MessageBox.Show("Some answers are incorrect.", "Math Quiz");
        }

        // Timer

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (timeLeft > 0)
            {
                timeLeft--;

                timeLabel.Text = timeLeft + " sec";

                if (timeLeft <= 5)
                {
                    timeLabel.BackColor = Color.Red;

                    timeLabel.ForeColor = Color.White;
                }
            }
            else
            {
                timer1.Stop();

                timeLabel.Text = "Time's up!";

                timeLabel.BackColor = Color.Red;

                timeLabel.ForeColor = Color.White;

                checkAnswersButton.Enabled = false;

                if (gameMode == "Continuous")
                {
                    MessageBox.Show("Time is up!\nQuestions completed: " + completedQuestions, "Math Quiz");
                }
                else
                {
                    MessageBox.Show("Time is up!", "Math Quiz");
                }
            }
        }

        // Answer Box

        private void Answer_Enter(object sender, EventArgs e)
        {
            NumericUpDown answerBox = sender as NumericUpDown;

            if (answerBox != null)
            {
                int lengthOfAnswer = answerBox.Value.ToString().Length;

                answerBox.Select(0, lengthOfAnswer);
            }
        }

        // Dark Theme

        private void DarkTheme(object sender, EventArgs e)
        {
            Backcolor = Color.Black;
            Frontcolor = Color.White;

            ApplyTheme();
        }

        // Bright Theme

        private void BrightTheme(
            object sender,
            EventArgs e)
        {
            Backcolor = Color.White;
            Frontcolor = Color.Black;

            ApplyTheme();
        }

        // Apply Theme

        private void ApplyTheme()
        {
            BackColor = Backcolor;
            ForeColor = Frontcolor;

            ApplyLabelTheme(title);

            ApplyButtonTheme(startGameButton);
            ApplyButtonTheme(settingsButton);

            ApplyLabelTheme(difficultyLabel);
            ApplyLabelTheme(gameModeLabel);
            ApplyLabelTheme(parametersLabel);

            ApplyLabelTheme(currentDifficultyLabel);
            ApplyLabelTheme(currentGamemodeLabel);
            ApplyLabelTheme(timeParameterLabel);
            ApplyLabelTheme(minimumParameterLabel);
            ApplyLabelTheme(maximumParameterLabel);

            ApplyButtonTheme(easyButton);
            ApplyButtonTheme(mediumButton);
            ApplyButtonTheme(hardButton);
            ApplyButtonTheme(customButton);

            ApplyButtonTheme(standardButton);
            ApplyButtonTheme(continuousButton);
            ApplyButtonTheme(backButton);

            ApplyNumericTheme(customTime);
            ApplyNumericTheme(customNumber);

            ApplyLabelTheme(timeLeftLabel);
            ApplyLabelTheme(completedQuestionsLabel);

            ApplyLabelTheme(question1Label);
            ApplyLabelTheme(question2Label);
            ApplyLabelTheme(question3Label);
            ApplyLabelTheme(question4Label);

            ApplyNumericTheme(answer1);
            ApplyNumericTheme(answer2);
            ApplyNumericTheme(answer3);
            ApplyNumericTheme(answer4);

            ApplyButtonTheme(checkAnswersButton);
            ApplyButtonTheme(mainMenuButton);


            if (timeLabel != null)
            {
                // Preserve red timer when five seconds or less remain.
                if (timeLeft <= 5 && timer1.Enabled)
                {
                    timeLabel.BackColor = Color.Red;
                    timeLabel.ForeColor = Color.White;
                }
                else if (timeLabel.Text == "Time's up!")
                {
                    timeLabel.BackColor = Color.Red;
                    timeLabel.ForeColor = Color.White;
                }
                else
                {
                    timeLabel.BackColor = Backcolor;
                    timeLabel.ForeColor = Frontcolor;
                }
            }
        }

        // Theme Methods

        private void ApplyLabelTheme(Label label)
        {
            if (label != null)
            {
                label.ForeColor = Frontcolor;
                label.BackColor = Backcolor;
            }
        }

        private void ApplyButtonTheme(Button button)
        {
            if (button != null)
            {
                button.BackColor = Backcolor;
                button.ForeColor = Frontcolor;
            }
        }

        private void ApplyNumericTheme(
            NumericUpDown numeric)
        {
            if (numeric != null)
            {
                numeric.BackColor = Backcolor;
                numeric.ForeColor = Frontcolor;
            }
        }
    }
}
