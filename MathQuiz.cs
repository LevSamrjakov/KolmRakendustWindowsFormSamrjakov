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
        Label currentDifficultyLabel, timeParameterLabel, minimumParameterLabel, maximumParameterLabel;
        Button easyButton, mediumButton, hardButton, customButton;
        Button standardButton, continuousButton;
        Button backButton;
        NumericUpDown customTime, customNumber;

        // Quiz

        Label timeLabel, timeLeftLabel;
        Label question1Label, question2Label, question3Label, question4Label;
        Label completedQuestionsLabel;

        NumericUpDown answer1, answer2, answer3, answer4;

        Button checkAnswersButton;

        // Timer

        Timer timer1;

        // Random

        Random randomizer = new Random();

        // Question data

        int question1Answer, question2Answer, question3Answer, question4Answer;

        // Difficulty

        string difficulty = "Easy";

        int gameTime = 60;
        int minimumNumber = 1;
        int maximumNumber = 10;

        // Game Mode

        string gameMode = "Standard";

        int completedQuestions = 0;

        // Time

        int timeLeft;

        // Theme

        Color Backcolor, Frontcolor;

        public MathQuiz(string title, int width, int height, Color Backcolor, Color Frontcolor)
        {
            // Constructor

            Text = title;
            Size = new Size(width, height);
            StartPosition = FormStartPosition.CenterScreen;

            // Window Theme / Colors

            this.Backcolor = BackColor = Backcolor;
            this.Frontcolor = ForeColor = Frontcolor;

            // Form Settings

            FormBorderStyle = FormBorderStyle.Fixed3D;
            MaximizeBox = false;

            // Timer

            timer1 = new Timer();
            timer1.Interval = 1000;
            timer1.Tick += Timer1_Tick;

            // Main Menu

            ShowMainMenu();

            // Settings Menu

            MainMenu menu = new MainMenu();

            MenuItem menuSettings = new MenuItem("Settings");

            MenuItem menuDarkTheme = new MenuItem("Dark Theme", new EventHandler(DarkTheme));
            menuSettings.MenuItems.Add(menuDarkTheme);

            MenuItem menuBrightTheme = new MenuItem("Bright Theme", new EventHandler(BrightTheme));
            menuSettings.MenuItems.Add(menuBrightTheme);

            menu.MenuItems.Add(menuSettings);

            Menu = menu;
        }

        // ==========================================
        // MAIN MENU
        // ==========================================

        private void ShowMainMenu()
        {
            Controls.Clear();

            // Title

            title = new Label();
            title.Text = "Math Quiz";
            title.Font = new Font("Arial", 30, FontStyle.Bold);
            title.AutoSize = true;
            title.Location = new Point(215, 70);
            Controls.Add(title);

            // Start Game Button

            startGameButton = new Button();
            startGameButton.Text = "Start Game";
            startGameButton.Font = new Font("Arial", 12, FontStyle.Bold);
            startGameButton.Size = new Size(160, 50);
            startGameButton.Location = new Point(220, 180);
            startGameButton.Click += StartGameButton_Click;
            Controls.Add(startGameButton);

            // Settings Button

            settingsButton = new Button();
            settingsButton.Text = "Settings";
            settingsButton.Font = new Font("Arial", 12, FontStyle.Bold);
            settingsButton.Size = new Size(160, 50);
            settingsButton.Location = new Point(220, 250);
            settingsButton.Click += SettingsButton_Click;
            Controls.Add(settingsButton);

            ApplyTheme();
        }

        // ==========================================
        // SETTINGS
        // ==========================================

        private void ShowSettings()
        {
            Controls.Clear();

            // Title

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
            currentDifficultyLabel.Text = "Difficulty: " + difficulty;
            currentDifficultyLabel.AutoSize = true;
            currentDifficultyLabel.Location = new Point(365, 135);
            Controls.Add(currentDifficultyLabel);

            timeParameterLabel = new Label();
            timeParameterLabel.Text = "Time: " + gameTime + " sec";
            timeParameterLabel.AutoSize = true;
            timeParameterLabel.Location = new Point(365, 165);
            Controls.Add(timeParameterLabel);

            minimumParameterLabel = new Label();
            minimumParameterLabel.Text = "Minimum: " + minimumNumber;
            minimumParameterLabel.AutoSize = true;
            minimumParameterLabel.Location = new Point(365, 195);
            Controls.Add(minimumParameterLabel);

            maximumParameterLabel = new Label();
            maximumParameterLabel.Text = "Maximum: " + maximumNumber;
            maximumParameterLabel.AutoSize = true;
            maximumParameterLabel.Location = new Point(365, 225);
            Controls.Add(maximumParameterLabel);

            // Custom Parameters

            customTime = new NumericUpDown();
            customTime.Minimum = 10;
            customTime.Maximum = 300;
            customTime.Value = gameTime;
            customTime.Size = new Size(100, 30);
            customTime.Location = new Point(365, 260);
            customTime.Visible = false;
            Controls.Add(customTime);

            customNumber = new NumericUpDown();
            customNumber.Minimum = 5;
            customNumber.Maximum = 1000;
            customNumber.Value = maximumNumber;
            customNumber.Size = new Size(100, 30);
            customNumber.Location = new Point(365, 300);
            customNumber.Visible = false;
            Controls.Add(customNumber);

            // Back

            backButton = new Button();
            backButton.Text = "Back";
            backButton.Size = new Size(100, 40);
            backButton.Location = new Point(250, 340);
            backButton.Click += BackButton_Click;
            Controls.Add(backButton);

            UpdateSettings();

            ApplyTheme();
        }

        // ==========================================
        // SETTINGS UPDATE
        // ==========================================

        private void UpdateSettings()
        {
            currentDifficultyLabel.Text = "Difficulty: " + difficulty;
            timeParameterLabel.Text = "Time: " + gameTime + " sec";
            minimumParameterLabel.Text = "Minimum: " + minimumNumber;
            maximumParameterLabel.Text = "Maximum: " + maximumNumber;

            if (difficulty == "Custom")
            {
                customTime.Visible = true;
                customNumber.Visible = true;

                customTime.Value = gameTime;
                customNumber.Value = maximumNumber;
            }
            else
            {
                customTime.Visible = false;
                customNumber.Visible = false;
            }
        }

        // ==========================================
        // DIFFICULTY
        // ==========================================

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

        // ==========================================
        // CUSTOM DIFFICULTY
        // ==========================================

        private void SetCustom()
        {
            difficulty = "Custom";

            UpdateSettings();
        }

        // ==========================================
        // GAME MODE
        // ==========================================

        private void SetStandard()
        {
            gameMode = "Standard";
        }

        private void SetContinuous()
        {
            gameMode = "Continuous";
        }

        // ==========================================
        // BUTTON EVENTS
        // ==========================================

        private void StartGameButton_Click(object sender, EventArgs e)
        {
            ShowQuiz();
            StartTheQuiz();
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

        private void CustomButton_Click(object sender, EventArgs e)
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

        // ==========================================
        // QUIZ
        // ==========================================

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

            // Timer Label

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
            completedQuestionsLabel.Text = "Questions completed: 0";
            completedQuestionsLabel.Font = new Font("Arial", 12, FontStyle.Bold);
            completedQuestionsLabel.AutoSize = true;
            completedQuestionsLabel.Location = new Point(80, 20);
            Controls.Add(completedQuestionsLabel);

            // Question 1

            question1Label = new Label();
            question1Label.Font = new Font("Arial", 18);
            question1Label.Size = new Size(250, 50);
            question1Label.Location = new Point(80, 80);
            question1Label.TextAlign = ContentAlignment.MiddleLeft;
            Controls.Add(question1Label);

            answer1 = new NumericUpDown();
            answer1.Font = new Font("Arial", 18);
            answer1.Size = new Size(100, 40);
            answer1.Location = new Point(350, 85);
            answer1.TabIndex = 1;
            answer1.Maximum = 100000;
            answer1.Enter += Answer_Enter;
            Controls.Add(answer1);

            // Question 2

            question2Label = new Label();
            question2Label.Font = new Font("Arial", 18);
            question2Label.Size = new Size(250, 50);
            question2Label.Location = new Point(80, 140);
            question2Label.TextAlign = ContentAlignment.MiddleLeft;
            Controls.Add(question2Label);

            answer2 = new NumericUpDown();
            answer2.Font = new Font("Arial", 18);
            answer2.Size = new Size(100, 40);
            answer2.Location = new Point(350, 145);
            answer2.TabIndex = 2;
            answer2.Maximum = 100000;
            answer2.Enter += Answer_Enter;
            Controls.Add(answer2);

            // Question 3

            question3Label = new Label();
            question3Label.Font = new Font("Arial", 18);
            question3Label.Size = new Size(250, 50);
            question3Label.Location = new Point(80, 200);
            question3Label.TextAlign = ContentAlignment.MiddleLeft;
            Controls.Add(question3Label);

            answer3 = new NumericUpDown();
            answer3.Font = new Font("Arial", 18);
            answer3.Size = new Size(100, 40);
            answer3.Location = new Point(350, 205);
            answer3.TabIndex = 3;
            answer3.Maximum = 100000;
            answer3.Enter += Answer_Enter;
            Controls.Add(answer3);

            // Question 4

            question4Label = new Label();
            question4Label.Font = new Font("Arial", 18);
            question4Label.Size = new Size(250, 50);
            question4Label.Location = new Point(80, 260);
            question4Label.TextAlign = ContentAlignment.MiddleLeft;
            Controls.Add(question4Label);

            answer4 = new NumericUpDown();
            answer4.Font = new Font("Arial", 18);
            answer4.Size = new Size(100, 40);
            answer4.Location = new Point(350, 265);
            answer4.TabIndex = 4;
            answer4.Maximum = 100000;
            answer4.Enter += Answer_Enter;
            Controls.Add(answer4);

            // Check Answers

            checkAnswersButton = new Button();
            checkAnswersButton.Text = "Check Answers";
            checkAnswersButton.Font = new Font("Arial", 12, FontStyle.Bold);
            checkAnswersButton.Size = new Size(180, 50);
            checkAnswersButton.Location = new Point(210, 340);
            checkAnswersButton.Click += CheckAnswersButton_Click;
            Controls.Add(checkAnswersButton);

            ApplyTheme();
        }

        // ==========================================
        // START QUIZ
        // ==========================================

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

            if (timer1.Enabled == false)
            {
                timeLeft = gameTime;
                timeLabel.Text = timeLeft + " sec";
                timeLabel.BackColor = Backcolor;
                timeLabel.ForeColor = Frontcolor;
                timer1.Start();
            }
        }

        // ==========================================
        // CREATE RANDOM QUESTION
        // ==========================================

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
                number2 = randomizer.Next(minimumNumber, maximumNumber + 1);

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

        // ==========================================
        // CHECK ANSWERS
        // ==========================================

        private void CheckAnswersButton_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled == false)
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

        // ==========================================
        // TIMER
        // ==========================================

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

        // ==========================================
        // ANSWER BOX
        // ==========================================

        private void Answer_Enter(object sender, EventArgs e)
        {
            NumericUpDown answerBox = sender as NumericUpDown;

            if (answerBox != null)
            {
                int lengthOfAnswer = answerBox.Value.ToString().Length;

                answerBox.Select(0, lengthOfAnswer);
            }
        }

        // ==========================================
        // DARK THEME
        // ==========================================

        private void DarkTheme(object sender, EventArgs e)
        {
            Backcolor = BackColor = Color.Black;
            Frontcolor = ForeColor = Color.White;

            ApplyTheme();
        }

        // ==========================================
        // BRIGHT THEME
        // ==========================================

        private void BrightTheme(object sender, EventArgs e)
        {
            Backcolor = BackColor = Color.White;
            Frontcolor = ForeColor = Color.Black;

            ApplyTheme();
        }

        // ==========================================
        // APPLY THEME
        // ==========================================

        private void ApplyTheme()
        {
            BackColor = Backcolor;
            ForeColor = Frontcolor;

            if (title != null)
            {
                title.ForeColor = Frontcolor;
            }

            if (startGameButton != null)
            {
                startGameButton.BackColor = Backcolor;
                startGameButton.ForeColor = Frontcolor;
            }

            if (settingsButton != null)
            {
                settingsButton.BackColor = Backcolor;
                settingsButton.ForeColor = Frontcolor;
            }

            if (difficultyLabel != null)
            {
                difficultyLabel.ForeColor = Frontcolor;
            }

            if (gameModeLabel != null)
            {
                gameModeLabel.ForeColor = Frontcolor;
            }

            if (parametersLabel != null)
            {
                parametersLabel.ForeColor = Frontcolor;
            }

            if (currentDifficultyLabel != null)
            {
                currentDifficultyLabel.ForeColor = Frontcolor;
            }

            if (timeParameterLabel != null)
            {
                timeParameterLabel.ForeColor = Frontcolor;
            }

            if (minimumParameterLabel != null)
            {
                minimumParameterLabel.ForeColor = Frontcolor;
            }

            if (maximumParameterLabel != null)
            {
                maximumParameterLabel.ForeColor = Frontcolor;
            }

            if (easyButton != null)
            {
                easyButton.BackColor = Backcolor;
                easyButton.ForeColor = Frontcolor;
            }

            if (mediumButton != null)
            {
                mediumButton.BackColor = Backcolor;
                mediumButton.ForeColor = Frontcolor;
            }

            if (hardButton != null)
            {
                hardButton.BackColor = Backcolor;
                hardButton.ForeColor = Frontcolor;
            }

            if (customButton != null)
            {
                customButton.BackColor = Backcolor;
                customButton.ForeColor = Frontcolor;
            }

            if (standardButton != null)
            {
                standardButton.BackColor = Backcolor;
                standardButton.ForeColor = Frontcolor;
            }

            if (continuousButton != null)
            {
                continuousButton.BackColor = Backcolor;
                continuousButton.ForeColor = Frontcolor;
            }

            if (backButton != null)
            {
                backButton.BackColor = Backcolor;
                backButton.ForeColor = Frontcolor;
            }

            if (customTime != null)
            {
                customTime.BackColor = Backcolor;
                customTime.ForeColor = Frontcolor;
            }

            if (customNumber != null)
            {
                customNumber.BackColor = Backcolor;
                customNumber.ForeColor = Frontcolor;
            }

            if (timeLeftLabel != null)
            {
                timeLeftLabel.ForeColor = Frontcolor;
            }

            if (timeLabel != null && timeLeft <= 5)
            {
                timeLabel.ForeColor = Color.White;
            }
            else if (timeLabel != null)
            {
                timeLabel.ForeColor = Frontcolor;
            }

            if (completedQuestionsLabel != null)
            {
                completedQuestionsLabel.ForeColor = Frontcolor;
            }

            if (question1Label != null)
            {
                question1Label.ForeColor = Frontcolor;
            }

            if (question2Label != null)
            {
                question2Label.ForeColor = Frontcolor;
            }

            if (question3Label != null)
            {
                question3Label.ForeColor = Frontcolor;
            }

            if (question4Label != null)
            {
                question4Label.ForeColor = Frontcolor;
            }

            if (answer1 != null)
            {
                answer1.BackColor = Backcolor;
                answer1.ForeColor = Frontcolor;
            }

            if (answer2 != null)
            {
                answer2.BackColor = Backcolor;
                answer2.ForeColor = Frontcolor;
            }

            if (answer3 != null)
            {
                answer3.BackColor = Backcolor;
                answer3.ForeColor = Frontcolor;
            }

            if (answer4 != null)
            {
                answer4.BackColor = Backcolor;
                answer4.ForeColor = Frontcolor;
            }

            if (checkAnswersButton != null)
            {
                checkAnswersButton.BackColor = Backcolor;
                checkAnswersButton.ForeColor = Frontcolor;
            }
        }
    }
}
