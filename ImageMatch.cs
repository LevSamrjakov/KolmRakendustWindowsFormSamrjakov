using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace KolmRakendustWindowsFormSamrjakov
{
    public partial class ImageMatch : Form
    {
        // Main Menu

        Label title;
        Button startGameButton, settingsButton;

        // Settings

        Label sizeLabel, parametersLabel;
        Label currentSizeLabel;

        Button size4Button, size8Button;
        Button changeImagesButton, backButton;

        // Game

        TableLayoutPanel gameTable;

        Label scoreLabel, matchesLabel;

        Button mainMenuButton;

        // Game Data

        int boardSize = 4;

        int score = 3200;

        int matchesFound = 0;

        List<string> imagePaths = new List<string>();

        List<PictureBox> cards =
            new List<PictureBox>();

        List<int> cardValues =
            new List<int>();

        List<bool> matchedCards =
            new List<bool>();

        PictureBox firstCard;
        PictureBox secondCard;

        int firstCardIndex = -1;
        int secondCardIndex = -1;

        // Timer

        Timer hideTimer;

        // Random

        Random random = new Random();

        // Theme

        Color Backcolor = Color.White;
        Color Frontcolor = Color.Black;

        // Constructor

        public ImageMatch(
            string title,
            int width,
            int height)
        {
            Text = title;

            Size =
                new Size(
                    width,
                    height);

            StartPosition =
                FormStartPosition.CenterScreen;

            // Form settings

            FormBorderStyle =
                FormBorderStyle.Fixed3D;

            MaximizeBox = false;

            // Default Images

            LoadDefaultImages();

            // Timer

            hideTimer =
                new Timer();

            hideTimer.Interval = 700;

            hideTimer.Tick +=
                HideTimer_Tick;

            // Main Menu

            ShowMainMenu();

            // Menu

            MainMenu menu =
                new MainMenu();

            MenuItem menuSettings =
                new MenuItem("Settings");

            MenuItem menuDarkTheme =
                new MenuItem(
                    "Dark Theme",
                    DarkTheme);

            MenuItem menuBrightTheme =
                new MenuItem(
                    "Bright Theme",
                    BrightTheme);

            menuSettings.MenuItems.Add(
                menuDarkTheme);

            menuSettings.MenuItems.Add(
                menuBrightTheme);

            menu.MenuItems.Add(
                menuSettings);

            Menu = menu;
        }

        // =========================================================
        // MAIN MENU
        // =========================================================

        private void ShowMainMenu()
        {
            Controls.Clear();

            title =
                new Label();

            title.Text =
                "Image Pairs Game";

            title.Font =
                new Font(
                    "Arial",
                    28,
                    FontStyle.Bold);

            title.AutoSize = false;

            title.TextAlign =
                ContentAlignment.MiddleCenter;

            title.Size =
                new Size(
                    500,
                    70);

            title.Location =
                new Point(
                    50,
                    60);

            Controls.Add(title);

            startGameButton =
                new Button();

            startGameButton.Text =
                "Start Game";

            startGameButton.Font =
                new Font(
                    "Arial",
                    12,
                    FontStyle.Bold);

            startGameButton.Size =
                new Size(
                    180,
                    50);

            startGameButton.Location =
                new Point(
                    210,
                    180);

            startGameButton.Click +=
                StartGameButton_Click;

            Controls.Add(
                startGameButton);

            settingsButton =
                new Button();

            settingsButton.Text =
                "Settings";

            settingsButton.Font =
                new Font(
                    "Arial",
                    12,
                    FontStyle.Bold);

            settingsButton.Size =
                new Size(
                    180,
                    50);

            settingsButton.Location =
                new Point(
                    210,
                    250);

            settingsButton.Click +=
                SettingsButton_Click;

            Controls.Add(
                settingsButton);

            ApplyTheme();
        }

        // =========================================================
        // SETTINGS
        // =========================================================

        private void ShowSettings()
        {
            Controls.Clear();

            title =
                new Label();

            title.Text =
                "Settings";

            title.Font =
                new Font(
                    "Arial",
                    28,
                    FontStyle.Bold);

            title.AutoSize = false;

            title.TextAlign =
                ContentAlignment.MiddleCenter;

            title.Size =
                new Size(
                    500,
                    60);

            title.Location =
                new Point(
                    50,
                    25);

            Controls.Add(title);

            // Board Size

            sizeLabel =
                new Label();

            sizeLabel.Text =
                "Board Size";

            sizeLabel.Font =
                new Font(
                    "Arial",
                    14,
                    FontStyle.Bold);

            sizeLabel.AutoSize = true;

            sizeLabel.Location =
                new Point(
                    50,
                    110);

            Controls.Add(
                sizeLabel);

            size4Button =
                new Button();

            size4Button.Text =
                "4 x 4";

            size4Button.Size =
                new Size(
                    120,
                    45);

            size4Button.Location =
                new Point(
                    50,
                    150);

            size4Button.Click +=
                Size4Button_Click;

            Controls.Add(
                size4Button);

            size8Button =
                new Button();

            size8Button.Text =
                "8 x 8";

            size8Button.Size =
                new Size(
                    120,
                    45);

            size8Button.Location =
                new Point(
                    50,
                    205);

            size8Button.Click +=
                Size8Button_Click;

            Controls.Add(
                size8Button);

            // Parameters

            parametersLabel =
                new Label();

            parametersLabel.Text =
                "Parameters";

            parametersLabel.Font =
                new Font(
                    "Arial",
                    14,
                    FontStyle.Bold);

            parametersLabel.AutoSize = true;

            parametersLabel.Location =
                new Point(
                    250,
                    110);

            Controls.Add(
                parametersLabel);

            currentSizeLabel =
                new Label();

            currentSizeLabel.AutoSize = true;

            currentSizeLabel.Location =
                new Point(
                    250,
                    155);

            Controls.Add(
                currentSizeLabel);

            // Change Images

            changeImagesButton =
                new Button();

            changeImagesButton.Text =
                "Change Images";

            changeImagesButton.Font =
                new Font(
                    "Arial",
                    10,
                    FontStyle.Bold);

            changeImagesButton.Size =
                new Size(
                    180,
                    45);

            changeImagesButton.Location =
                new Point(
                    200,
                    230);

            changeImagesButton.Click +=
                ChangeImagesButton_Click;

            Controls.Add(
                changeImagesButton);

            // Back

            backButton =
                new Button();

            backButton.Text =
                "Back";

            backButton.Size =
                new Size(
                    120,
                    45);

            backButton.Location =
                new Point(
                    240,
                    310);

            backButton.Click +=
                BackButton_Click;

            Controls.Add(
                backButton);

            UpdateSettings();

            ApplyTheme();
        }

        private void UpdateSettings()
        {
            if (currentSizeLabel == null)
            {
                return;
            }

            currentSizeLabel.Text =
                "Board size: " +
                boardSize +
                " x " +
                boardSize;

            if (boardSize == 4)
            {
                currentSizeLabel.Text +=
                    "\nPairs: 8";
            }
            else
            {
                currentSizeLabel.Text +=
                    "\nPairs: 32";
            }
        }

        // =========================================================
        // SETTINGS BUTTONS
        // =========================================================

        private void SettingsButton_Click(
            object sender,
            EventArgs e)
        {
            ShowSettings();
        }

        private void Size4Button_Click(
            object sender,
            EventArgs e)
        {
            boardSize = 4;

            UpdateSettings();
        }

        private void Size8Button_Click(
            object sender,
            EventArgs e)
        {
            boardSize = 8;

            UpdateSettings();
        }

        private void BackButton_Click(
            object sender,
            EventArgs e)
        {
            ShowMainMenu();
        }

        private void ChangeImagesButton_Click(
            object sender,
            EventArgs e)
        {
            ImageSelection imageSelection =
                new ImageSelection(
                    imagePaths,
                    boardSize);

            if (imageSelection.ShowDialog() ==
                DialogResult.OK)
            {
                imagePaths =
                    imageSelection.SelectedImages;
            }
        }

        // =========================================================
        // START GAME
        // =========================================================

        private void StartGameButton_Click(
            object sender,
            EventArgs e)
        {
            StartGame();
        }

        private void StartGame()
        {
            hideTimer.Stop();

            score = 3200;

            matchesFound = 0;

            firstCard = null;
            secondCard = null;

            firstCardIndex = -1;
            secondCardIndex = -1;

            Controls.Clear();

            CreateGameBoard();

            CreateCards();
        }

        // =========================================================
        // GAME BOARD
        // =========================================================

        private void CreateGameBoard()
        {
            scoreLabel =
                new Label();

            scoreLabel.Text =
                "Score: " + score;

            scoreLabel.Font =
                new Font(
                    "Arial",
                    12,
                    FontStyle.Bold);

            scoreLabel.AutoSize = true;

            scoreLabel.Location =
                new Point(
                    15,
                    15);

            Controls.Add(
                scoreLabel);

            matchesLabel =
                new Label();

            matchesLabel.Text =
                "Matches: 0 / " +
                ((boardSize * boardSize) / 2);

            matchesLabel.Font =
                new Font(
                    "Arial",
                    12,
                    FontStyle.Bold);

            matchesLabel.AutoSize = true;

            matchesLabel.Location =
                new Point(
                    180,
                    15);

            Controls.Add(
                matchesLabel);

            mainMenuButton =
                new Button();

            mainMenuButton.Text =
                "Main Menu";

            mainMenuButton.Size =
                new Size(
                    100,
                    30);

            mainMenuButton.Location =
                new Point(
                    ClientSize.Width - 120,
                    10);

            mainMenuButton.Click +=
                MainMenuGameButton_Click;

            Controls.Add(
                mainMenuButton);

            gameTable =
                new TableLayoutPanel();

            gameTable.RowCount =
                boardSize;

            gameTable.ColumnCount =
                boardSize;

            gameTable.Location =
                new Point(
                    15,
                    55);

            int boardWidth =
                ClientSize.Width - 45;

            int boardHeight =
                ClientSize.Height - 105;

            gameTable.Size =
                new Size(
                    boardWidth,
                    boardHeight);

            gameTable.CellBorderStyle =
                TableLayoutPanelCellBorderStyle.Single;

            gameTable.BackColor =
                Color.Gray;

            for (int i = 0;
                 i < boardSize;
                 i++)
            {
                gameTable.RowStyles.Add(
                    new RowStyle(
                        SizeType.Percent,
                        100f / boardSize));

                gameTable.ColumnStyles.Add(
                    new ColumnStyle(
                        SizeType.Percent,
                        100f / boardSize));
            }

            Controls.Add(
                gameTable);

            ApplyTheme();
        }

        // =========================================================
        // CARDS
        // =========================================================

        private void CreateCards()
        {
            cards.Clear();

            cardValues.Clear();

            matchedCards.Clear();

            int numberOfCards =
                boardSize * boardSize;

            int numberOfPairs =
                numberOfCards / 2;

            while (imagePaths.Count <
                   numberOfPairs)
            {
                imagePaths.Add("");
            }

            List<int> values =
                new List<int>();

            for (int i = 0;
                 i < numberOfPairs;
                 i++)
            {
                values.Add(i);
                values.Add(i);
            }

            // Shuffle

            for (int i =
                     values.Count - 1;
                 i > 0;
                 i--)
            {
                int j =
                    random.Next(
                        i + 1);

                int temporary =
                    values[i];

                values[i] =
                    values[j];

                values[j] =
                    temporary;
            }

            // Create Cards

            for (int i = 0;
                 i < numberOfCards;
                 i++)
            {
                PictureBox card =
                    new PictureBox();

                card.Dock =
                    DockStyle.Fill;

                card.Margin =
                    new Padding(3);

                card.SizeMode =
                    PictureBoxSizeMode.Zoom;

                card.BackColor =
                    Color.DarkBlue;

                card.BorderStyle =
                    BorderStyle.FixedSingle;

                card.Cursor =
                    Cursors.Hand;

                card.Tag = i;

                card.Click +=
                    Card_Click;

                cards.Add(card);

                cardValues.Add(
                    values[i]);

                matchedCards.Add(
                    false);

                gameTable.Controls.Add(
                    card,
                    i % boardSize,
                    i / boardSize);

                ShowCardBack(card);
            }
        }

        // =========================================================
        // CARD CLICK
        // =========================================================

        private void Card_Click(
            object sender,
            EventArgs e)
        {
            if (hideTimer.Enabled)
            {
                return;
            }

            PictureBox card =
                (PictureBox)sender;

            int index =
                (int)card.Tag;

            if (matchedCards[index])
            {
                return;
            }

            if (index == firstCardIndex)
            {
                return;
            }

            ShowCardImage(
                card,
                cardValues[index]);

            if (firstCard == null)
            {
                firstCard = card;

                firstCardIndex =
                    index;

                return;
            }

            secondCard = card;

            secondCardIndex =
                index;

            // Match

            if (cardValues[firstCardIndex] ==
                cardValues[secondCardIndex])
            {
                matchedCards[firstCardIndex] =
                    true;

                matchedCards[secondCardIndex] =
                    true;

                matchesFound++;

                score += 200;

                UpdateScore();

                firstCard = null;
                secondCard = null;

                firstCardIndex = -1;
                secondCardIndex = -1;

                if (matchesFound ==
                    (boardSize * boardSize) / 2)
                {
                    GameWon();
                }
            }
            else
            {
                score -= 100;

                if (score < 0)
                {
                    score = 0;
                }

                UpdateScore();

                hideTimer.Start();
            }
        }

        // =========================================================
        // HIDE CARDS
        // =========================================================

        private void HideTimer_Tick(
            object sender,
            EventArgs e)
        {
            hideTimer.Stop();

            if (firstCard != null)
            {
                ShowCardBack(firstCard);
            }

            if (secondCard != null)
            {
                ShowCardBack(secondCard);
            }

            firstCard = null;
            secondCard = null;

            firstCardIndex = -1;
            secondCardIndex = -1;
        }

        // =========================================================
        // SHOW CARD
        // =========================================================

        private void ShowCardBack(
            PictureBox card)
        {
            card.Image = null;

            card.BackColor =
                Color.DarkBlue;
        }

        private void ShowCardImage(
            PictureBox card,
            int imageNumber)
        {
            if (imageNumber >= 0 &&
                imageNumber < imagePaths.Count &&
                File.Exists(
                    imagePaths[imageNumber]))
            {
                using (Image temporary =
                    Image.FromFile(
                        imagePaths[imageNumber]))
                {
                    card.Image =
                        new Bitmap(
                            temporary);
                }

                card.BackColor =
                    Color.White;
            }
            else
            {
                Bitmap bitmap =
                    new Bitmap(
                        100,
                        100);

                using (Graphics graphics =
                    Graphics.FromImage(bitmap))
                {
                    graphics.Clear(
                        Color.White);

                    string text =
                        "Image " +
                        (imageNumber + 1);

                    using (Font font =
                        new Font(
                            "Arial",
                            16,
                            FontStyle.Bold))
                    {
                        SizeF size =
                            graphics.MeasureString(
                                text,
                                font);

                        graphics.DrawString(
                            text,
                            font,
                            Brushes.Black,
                            (100 - size.Width) / 2,
                            (100 - size.Height) / 2);
                    }
                }

                card.Image =
                    bitmap;

                card.BackColor =
                    Color.White;
            }
        }

        // =========================================================
        // SCORE
        // =========================================================

        private void UpdateScore()
        {
            if (scoreLabel != null)
            {
                scoreLabel.Text =
                    "Score: " + score;
            }

            if (matchesLabel != null)
            {
                matchesLabel.Text =
                    "Matches: " +
                    matchesFound +
                    " / " +
                    ((boardSize * boardSize) / 2);
            }
        }

        // =========================================================
        // GAME WON
        // =========================================================

        private void GameWon()
        {
            hideTimer.Stop();

            MessageBox.Show(
                "You won!\n\nFinal score: " +
                score,
                "Image Pairs Game");

            ShowGameResult();
        }

        private void ShowGameResult()
        {
            Controls.Clear();

            Label resultLabel =
                new Label();

            resultLabel.Text =
                "YOU WON!\n\nScore: " +
                score;

            resultLabel.Font =
                new Font(
                    "Arial",
                    24,
                    FontStyle.Bold);

            resultLabel.TextAlign =
                ContentAlignment.MiddleCenter;

            resultLabel.Size =
                new Size(
                    500,
                    160);

            resultLabel.Location =
                new Point(
                    50,
                    100);

            Controls.Add(
                resultLabel);

            Button playAgainButton =
                new Button();

            playAgainButton.Text =
                "Play Again";

            playAgainButton.Font =
                new Font(
                    "Arial",
                    11,
                    FontStyle.Bold);

            playAgainButton.Size =
                new Size(
                    160,
                    45);

            playAgainButton.Location =
                new Point(
                    120,
                    300);

            playAgainButton.Click +=
                PlayAgainButton_Click;

            Controls.Add(
                playAgainButton);

            Button resultMainMenuButton =
                new Button();

            resultMainMenuButton.Text =
                "Main Menu";

            resultMainMenuButton.Font =
                new Font(
                    "Arial",
                    11,
                    FontStyle.Bold);

            resultMainMenuButton.Size =
                new Size(
                    160,
                    45);

            resultMainMenuButton.Location =
                new Point(
                    310,
                    300);

            resultMainMenuButton.Click +=
                MainMenuGameButton_Click;

            Controls.Add(
                resultMainMenuButton);

            ApplyTheme();
        }

        private void PlayAgainButton_Click(
            object sender,
            EventArgs e)
        {
            StartGame();
        }

        // =========================================================
        // MAIN MENU DURING GAME
        // =========================================================

        private void MainMenuGameButton_Click(
            object sender,
            EventArgs e)
        {
            hideTimer.Stop();

            ShowMainMenu();
        }

        // =========================================================
        // DEFAULT IMAGES
        // =========================================================

        private void LoadDefaultImages()
        {
            imagePaths.Clear();

            string imageFolder =
                Path.GetFullPath(
                    @"..\..\Images");

            if (!Directory.Exists(
                imageFolder))
            {
                return;
            }

            string[] files =
                Directory.GetFiles(
                    imageFolder);

            foreach (string file in files)
            {
                string extension =
                    Path.GetExtension(file)
                    .ToLower();

                if (IsImageFile(extension))
                {
                    imagePaths.Add(file);
                }
            }
        }

        private bool IsImageFile(
            string extension)
        {
            return
                extension == ".jpg" ||
                extension == ".jpeg" ||
                extension == ".png" ||
                extension == ".bmp" ||
                extension == ".gif";
        }

        // =========================================================
        // THEME
        // =========================================================

        private void DarkTheme(
            object sender,
            EventArgs e)
        {
            Backcolor = Color.Black;

            Frontcolor = Color.White;

            ApplyTheme();
        }

        private void BrightTheme(
            object sender,
            EventArgs e)
        {
            Backcolor = Color.White;

            Frontcolor = Color.Black;

            ApplyTheme();
        }

        private void ApplyTheme()
        {
            BackColor = Backcolor;

            ForeColor = Frontcolor;

            ApplyLabelTheme(title);

            ApplyButtonTheme(startGameButton);
            ApplyButtonTheme(settingsButton);

            ApplyLabelTheme(sizeLabel);
            ApplyLabelTheme(parametersLabel);
            ApplyLabelTheme(currentSizeLabel);

            ApplyButtonTheme(size4Button);
            ApplyButtonTheme(size8Button);
            ApplyButtonTheme(changeImagesButton);
            ApplyButtonTheme(backButton);

            ApplyLabelTheme(scoreLabel);
            ApplyLabelTheme(matchesLabel);

            ApplyButtonTheme(mainMenuButton);
        }

        private void ApplyLabelTheme(
            Label label)
        {
            if (label != null)
            {
                label.BackColor =
                    Backcolor;

                label.ForeColor =
                    Frontcolor;
            }
        }

        private void ApplyButtonTheme(
            Button button)
        {
            if (button != null)
            {
                button.BackColor =
                    Backcolor;

                button.ForeColor =
                    Frontcolor;
            }
        }
    }


    // =============================================================
    // IMAGE SELECTION WINDOW
    // =============================================================

    public class ImageSelection : Form
    {
        // Images

        List<string> selectedImages =
            new List<string>();

        List<PictureBox> previews =
            new List<PictureBox>();

        List<TextBox> pathBoxes =
            new List<TextBox>();

        int requiredImages;

        // Controls

        FlowLayoutPanel imagePanel;

        Button saveButton;

        // Selected Images

        public List<string> SelectedImages
        {
            get
            {
                return selectedImages;
            }
        }

        // =========================================================
        // CONSTRUCTOR
        // =========================================================

        public ImageSelection(
            List<string> currentImages,
            int boardSize)
        {
            Text =
                "Change Game Images";

            Size =
                new Size(
                    800,
                    600);

            StartPosition =
                FormStartPosition.CenterScreen;

            requiredImages =
                (boardSize * boardSize) / 2;

            // Copy current images

            for (int i = 0;
                 i < currentImages.Count &&
                 i < requiredImages;
                 i++)
            {
                selectedImages.Add(
                    currentImages[i]);
            }

            while (selectedImages.Count <
                   requiredImages)
            {
                selectedImages.Add("");
            }

            CreateInterface();

            RefreshImages();
        }

        // =========================================================
        // INTERFACE
        // =========================================================

        private void CreateInterface()
        {
            Label title =
                new Label();

            title.Text =
                "Select Images";

            title.Font =
                new Font(
                    "Arial",
                    20,
                    FontStyle.Bold);

            title.Size =
                new Size(
                    760,
                    45);

            title.Location =
                new Point(
                    20,
                    15);

            title.TextAlign =
                ContentAlignment.MiddleCenter;

            Controls.Add(
                title);

            Label information =
                new Label();

            information.Text =
                "Choose one image for each pair.";

            information.AutoSize = true;

            information.Location =
                new Point(
                    20,
                    65);

            Controls.Add(
                information);

            // IMPORTANT:
            // The old Select Folder and Add Image buttons
            // have been removed.

            // Save

            saveButton =
                new Button();

            saveButton.Text =
                "Save";

            saveButton.Size =
                new Size(
                    120,
                    35);

            saveButton.Location =
                new Point(
                    630,
                    90);

            saveButton.Click +=
                SaveButton_Click;

            Controls.Add(
                saveButton);

            // Image Panel

            imagePanel =
                new FlowLayoutPanel();

            imagePanel.Location =
                new Point(
                    20,
                    140);

            imagePanel.Size =
                new Size(
                    750,
                    390);

            imagePanel.AutoScroll = true;

            imagePanel.FlowDirection =
                FlowDirection.TopDown;

            imagePanel.WrapContents = false;

            Controls.Add(
                imagePanel);
        }

        // =========================================================
        // REFRESH IMAGES
        // =========================================================

        private void RefreshImages()
        {
            imagePanel.Controls.Clear();

            previews.Clear();

            pathBoxes.Clear();

            for (int i = 0;
                 i < requiredImages;
                 i++)
            {
                Panel row =
                    new Panel();

                row.Size =
                    new Size(
                        700,
                        90);

                // Image Number

                Label number =
                    new Label();

                number.Text =
                    "Image " + (i + 1);

                number.Font =
                    new Font(
                        "Arial",
                        10,
                        FontStyle.Bold);

                number.Size =
                    new Size(
                        80,
                        30);

                number.Location =
                    new Point(
                        0,
                        30);

                row.Controls.Add(
                    number);

                // Preview

                PictureBox preview =
                    new PictureBox();

                preview.Size =
                    new Size(
                        80,
                        80);

                preview.Location =
                    new Point(
                        90,
                        5);

                preview.SizeMode =
                    PictureBoxSizeMode.Zoom;

                preview.BorderStyle =
                    BorderStyle.FixedSingle;

                row.Controls.Add(
                    preview);

                previews.Add(
                    preview);

                // Path

                TextBox pathBox =
                    new TextBox();

                pathBox.Size =
                    new Size(
                        400,
                        25);

                pathBox.Location =
                    new Point(
                        185,
                        30);

                pathBox.Text =
                    selectedImages[i];

                row.Controls.Add(
                    pathBox);

                pathBoxes.Add(
                    pathBox);

                // Select

                Button select =
                    new Button();

                select.Text =
                    "Select";

                select.Size =
                    new Size(
                        80,
                        30);

                select.Location =
                    new Point(
                        600,
                        28);

                int index = i;

                select.Click +=
                    delegate
                    {
                        SelectImageSource(index);
                    };

                row.Controls.Add(
                    select);

                imagePanel.Controls.Add(
                    row);

                LoadPreview(i);
            }
        }

        // =========================================================
        // SELECT IMAGE SOURCE
        // =========================================================

        private void SelectImageSource(
            int index)
        {
            ImageSourceSelection sourceSelection =
                new ImageSourceSelection();

            if (sourceSelection.ShowDialog() ==
                DialogResult.OK)
            {
                string selectedPath =
                    sourceSelection.SelectedImagePath;

                if (!string.IsNullOrEmpty(
                    selectedPath))
                {
                    selectedImages[index] =
                        selectedPath;

                    pathBoxes[index].Text =
                        selectedPath;

                    LoadPreview(index);
                }
            }
        }

        // =========================================================
        // PREVIEW
        // =========================================================

        private void LoadPreview(
            int index)
        {
            if (index < 0 ||
                index >= previews.Count)
            {
                return;
            }

            // Dispose old image

            if (previews[index].Image != null)
            {
                previews[index].Image.Dispose();

                previews[index].Image =
                    null;
            }

            if (index >=
                selectedImages.Count)
            {
                return;
            }

            string path =
                selectedImages[index];

            if (!string.IsNullOrEmpty(path) &&
                File.Exists(path))
            {
                try
                {
                    using (Image image =
                        Image.FromFile(path))
                    {
                        previews[index].Image =
                            new Bitmap(image);
                    }
                }
                catch
                {
                    previews[index].Image =
                        null;
                }
            }
        }

        // =========================================================
        // SAVE
        // =========================================================

        private void SaveButton_Click(
            object sender,
            EventArgs e)
        {
            for (int i = 0;
                 i < requiredImages;
                 i++)
            {
                selectedImages[i] =
                    pathBoxes[i].Text;
            }

            DialogResult =
                DialogResult.OK;

            Close();
        }
    }


    // =============================================================
    // IMAGE SOURCE SELECTION
    // =============================================================

    public class ImageSourceSelection : Form
    {
        // The image selected by this window.

        public string SelectedImagePath
        {
            get;
            private set;
        }

        Button pcButton;
        Button projectImagesButton;

        // =========================================================
        // CONSTRUCTOR
        // =========================================================

        public ImageSourceSelection()
        {
            Text =
                "Choose Image";

            Size =
                new Size(
                    330,
                    180);

            StartPosition =
                FormStartPosition.CenterParent;

            FormBorderStyle =
                FormBorderStyle.FixedDialog;

            MaximizeBox = false;

            MinimizeBox = false;

            CreateInterface();
        }

        // =========================================================
        // INTERFACE
        // =========================================================

        private void CreateInterface()
        {
            Label title =
                new Label();

            title.Text =
                "Choose image source";

            title.Font =
                new Font(
                    "Arial",
                    12,
                    FontStyle.Bold);

            title.AutoSize = false;

            title.TextAlign =
                ContentAlignment.MiddleCenter;

            title.Size =
                new Size(
                    290,
                    35);

            title.Location =
                new Point(
                    20,
                    15);

            Controls.Add(
                title);

            // PC Button

            pcButton =
                new Button();

            pcButton.Text =
                "Choose from PC";

            pcButton.Size =
                new Size(
                    125,
                    50);

            pcButton.Location =
                new Point(
                    25,
                    70);

            pcButton.Click +=
                PcButton_Click;

            Controls.Add(
                pcButton);

            // Project Images Button

            projectImagesButton =
                new Button();

            projectImagesButton.Text =
                "Project Images";

            projectImagesButton.Size =
                new Size(
                    125,
                    50);

            projectImagesButton.Location =
                new Point(
                    175,
                    70);

            projectImagesButton.Click +=
                ProjectImagesButton_Click;

            Controls.Add(
                projectImagesButton);
        }

        // =========================================================
        // CHOOSE FROM PC
        // =========================================================

        private void PcButton_Click(
            object sender,
            EventArgs e)
        {
            using (OpenFileDialog dialog =
                new OpenFileDialog())
            {
                dialog.Title =
                    "Choose an image";

                dialog.Filter =
                    "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

                dialog.Multiselect = false;

                if (dialog.ShowDialog() ==
                    DialogResult.OK)
                {
                    SelectedImagePath =
                        dialog.FileName;

                    DialogResult =
                        DialogResult.OK;

                    Close();
                }
            }
        }

        // =========================================================
        // CHOOSE FROM PROJECT IMAGES
        // =========================================================

        private void ProjectImagesButton_Click(
            object sender,
            EventArgs e)
        {
            ProjectImageGallery gallery =
                new ProjectImageGallery();

            if (gallery.ShowDialog(this) ==
                DialogResult.OK)
            {
                SelectedImagePath =
                    gallery.SelectedImagePath;

                DialogResult =
                    DialogResult.OK;

                Close();
            }
        }
    }


    // =============================================================
    // PROJECT IMAGE GALLERY
    // =============================================================

    public class ProjectImageGallery : Form
    {
        FlowLayoutPanel imagePanel;

        public string SelectedImagePath
        {
            get;
            private set;
        }

        // =========================================================
        // CONSTRUCTOR
        // =========================================================

        public ProjectImageGallery()
        {
            Text =
                "Project Images";

            Size =
                new Size(
                    600,
                    400);

            StartPosition =
                FormStartPosition.CenterParent;

            CreateInterface();

            LoadImages();
        }

        // Interface

        private void CreateInterface()
        {
            imagePanel = new FlowLayoutPanel();

            imagePanel.Dock = DockStyle.Fill;

            imagePanel.AutoScroll = true;

            imagePanel.Padding = new Padding(5);

            imagePanel.WrapContents = true;

            imagePanel.FlowDirection = FlowDirection.LeftToRight;

            Controls.Add(imagePanel);
        }

        // Load Images (From Project's Image Folder)

        private void LoadImages()
        {
            string imageFolder = Path.GetFullPath(@"..\..\Images");

            if (!Directory.Exists(imageFolder))
            {
                Label noImages = new Label();

                noImages.Text = "The project Images folder does not exist.";

                noImages.AutoSize = true;

                noImages.Location = new Point(20, 20);

                imagePanel.Controls.Add(noImages);

                return;
            }

            string[] images = Directory.GetFiles(imageFolder);

            foreach (string imagePath in images)
            {
                string extension = Path.GetExtension(imagePath).ToLower();

                if (!IsImageFile(extension))
                {
                    continue;
                }

                CreateImageBox(imagePath);
            }

            if (imagePanel.Controls.Count == 0)
            {
                Label noImages = new Label();

                noImages.Text = "There are no images in the project Images folder.";

                noImages.AutoSize = true;

                noImages.Location = new Point(20, 20);

                imagePanel.Controls.Add(noImages);
            }
        }

        // Image Box Creation

        private void CreateImageBox(string imagePath)
        {
            PictureBox imageBox = new PictureBox();

            imageBox.Size = new Size(120, 100);

            imageBox.SizeMode = PictureBoxSizeMode.Zoom;

            imageBox.BorderStyle = BorderStyle.FixedSingle;

            imageBox.Cursor = Cursors.Hand;

            imageBox.Tag = imagePath;

            try
            {
                // Make a copy so the file isn't locked.

                using (Image image = Image.FromFile(imagePath))
                {
                    imageBox.Image = new Bitmap(image);
                }
            }
            catch
            {
                return;
            }

            imageBox.Click += ImageBox_Click;

            imagePanel.Controls.Add(imageBox);
        }

        // Image Click

        private void ImageBox_Click(object sender, EventArgs e)
        {
            PictureBox imageBox = (PictureBox)sender;

            SelectedImagePath = (string)imageBox.Tag;

            DialogResult = DialogResult.OK;

            Close();
        }

        // Image File Check

        private bool IsImageFile(string extension)
        {
            return
                extension == ".jpg" ||
                extension == ".jpeg" ||
                extension == ".png" ||
                extension == ".bmp" ||
                extension == ".gif";
        }
    }
}
