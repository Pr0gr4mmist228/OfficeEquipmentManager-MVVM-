using Microsoft.VisualBasic;
using OfficeEquipmentManager.LocalDB;
using OfficeEquipmentManager.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Excel = Microsoft.Office.Interop.Excel;
using MessageBox = System.Windows.MessageBox;
using Mb = System.Windows.MessageBox;
using System.Windows.Controls;
using Control = System.Windows.Controls;
using Grid = System.Windows.Controls.Grid;
using HorizontalAlignment = System.Windows.HorizontalAlignment;
using Word = Microsoft.Office.Interop.Word;
using System.Data.Linq;

namespace OfficeEquipmentManager.ViewModel
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        public string IconPath { get { return Directory.GetCurrentDirectory() + @"/icon.ico"; } }
        public ObservableCollection<User> Users { get; set; }
        public ObservableCollection<Administrator> Administrators { get; set; }
        public ObservableCollection<Barcode> Barcodes { get; set; }
        public ObservableCollection<Equipment> Equipment { get; set; }

        private static Equipment selectedEquipment;
        public static Equipment SelectedEquipment { get { return selectedEquipment; } set { selectedEquipment = value; } }

        private Equipment _selectedEquipment;
        public  Equipment _SelectedEquipment { get { return _selectedEquipment; } set { _selectedEquipment = value; SelectedEquipment = value; OnPropertyChanged("_SelectedEquipment"); } }

        public ObservableCollection<EquipmentCategory> EquipmentCategories { get; set; }

        private EquipmentCategory selectedCategory;
        public EquipmentCategory SelectedCategory { get { return selectedCategory; } set { selectedCategory = value; OnPropertyChanged("SelectedCategory"); } }

        public ObservableCollection<EquipmentStatus> EquipmentStatuses { get; set; }

        public EquipmentStatus SelectedStatus { get { return selectedStatus; } set { selectedStatus = value; OnPropertyChanged("SelectedStatus"); } }
        private static EquipmentStatus selectedStatus;

        public ObservableCollection<Role> Roles { get; set; }
        public ObservableCollection<Booker> Bookers { get; set; }

        public ApplicationViewModel()
        {
            ContextConnector.db.User.Load();
            Users = ContextConnector.db.User.Local;

            ContextConnector.db.Administrator.Load();
            Administrators = ContextConnector.db.Administrator.Local;

            ContextConnector.db.Barcode.Load();
            Barcodes = ContextConnector.db.Barcode.Local;

            ContextConnector.db.EquipmentCategory.Load();
            EquipmentCategories = ContextConnector.db.EquipmentCategory.Local;

            ContextConnector.db.Administrator.Load();
            Administrators = ContextConnector.db.Administrator.Local;

            ContextConnector.db.Barcode.Load();
            Barcodes = ContextConnector.db.Barcode.Local;

            ContextConnector.db.Equipment.Load();
            Equipment = ContextConnector.db.Equipment.Local;

            ContextConnector.db.EquipmentStatus.Load();
            EquipmentStatuses = ContextConnector.db.EquipmentStatus.Local;

            ContextConnector.db.Role.Load();
            Roles = ContextConnector.db.Role.Local;

            ContextConnector.db.Booker.Load();
            Bookers = ContextConnector.db.Booker.Local;
        }

        public string Password { get { return password; } set { password = value; OnPropertyChanged("Password"); } }
        private string password;
        private string login;
        public string Login { get { return login; } set { login = value; OnPropertyChanged("Login"); } }

        public RelayCommand AuthCommand
        {
            get
            {
                return new RelayCommand(obj => AuthorizationCheck());
            }
        }
        public ListBoxItem SelectedItemWelcomePage { get { 
                if (_selectedItemWelcomePage != null) {
                    if (_oldSelectedItem != null)
                    {
                        _oldSelectedItem.FontWeight = FontWeights.Normal;
                        _oldSelectedItem.BorderThickness = new Thickness(0, 0, 0, 0);
                    }
                    
                    _oldSelectedItem = _selectedItemWelcomePage; 
                    _selectedItemWelcomePage.FontWeight = FontWeights.Bold; 
                    _selectedItemWelcomePage.BorderThickness = new Thickness(5,0,0,0);
                    _selectedItemWelcomePage.BorderBrush = Brushes.White; 
                    return _selectedItemWelcomePage; } 
                return null; 
            } 
            set { _selectedItemWelcomePage = value; OnPropertyChanged("SelectedItemWelcomePage"); } }

        private ListBoxItem _selectedItemWelcomePage;
        private ListBoxItem _oldSelectedItem;

        public static User CurrentUser { get; set; }

        private void AuthorizationCheck()
        {
            User user = Users.FirstOrDefault(x => x.Login == Login && x.Password == Password);
            if (!String.IsNullOrEmpty(Password) && !String.IsNullOrEmpty(Login))
            {
                if (user != null)
                {
                    if (user.RoleId == 1) { Frames.MainFrame.Navigate(new AdministratorResourses.AdminMenuPage(user)); CurrentUser = user; };
                    if (user.RoleId == 2) { Frames.MainFrame.Navigate(new BookerResourses.BookerMenuPage(user)); CurrentUser = user; };
                }
                else MessageBox.Show("Неверно введен пароль или логин", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else MessageBox.Show("Введите логин и пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public RelayCommand NavigateCommand { get { return new RelayCommand(obj => Navigate(obj as String)); } }

        private void Navigate(string navigationSourceString)
        {
            Type userType = Type.GetType("OfficeEquipmentManager." + navigationSourceString);
            object navigationSource = Activator.CreateInstance(userType);
            Frames.MainFrame.Navigate(navigationSource);
        }

        public static Window CurrentWindow { get; set; }

        public RelayCommand OpenWindowCommand { get { return new RelayCommand(obj => OpenWindow(obj as String)); } }

        private void OpenWindow(string windowSource)
        {
            Type userType = Type.GetType("OfficeEquipmentManager." + windowSource);
            object navigationSource = Activator.CreateInstance(userType);
            System.Windows.Window window = (navigationSource as System.Windows.Window);
            CurrentWindow = window;
            window.ShowDialog();
        }

        public RelayCommand AddCategoryCommmand
        {
            get
            {
                EquipmentCategory category = null;
                return new RelayCommand(obj =>
                {
                    category = new EquipmentCategory
                    {
                        Name = obj as String
                    };
                    EquipmentCategories.Add(category);
                    MessageBox.Show("Категория успешно добавлена", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    CurrentWindow.Close();
                });
            }
        }

        public RelayCommand DeleteCategoryCommand { get { return new RelayCommand(obj => {
            EquipmentCategories.Remove(SelectedCategory);
        }); } }

        public RelayCommand AddFromExcelCommand
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    OpenFileDialog txtDialog = new OpenFileDialog
                    {
                        Multiselect = false,
                        Filter = "Файл Excel | *xlsx"
                    };
                    txtDialog.ShowDialog();

                    var excelBook = new Excel.Application();
                    excelBook.Workbooks.Open(txtDialog.FileName);

                    Excel.Worksheet worksheet = excelBook.Worksheets.Item[1];
                    int columns = worksheet.UsedRange.Columns.Count;
                    int rows = worksheet.UsedRange.Rows.Count;

                    for (int i = 2; i <= rows; i++)
                    {
                        object[] equipmentValues = new object[7];

                        for (int j = 1; j <= columns; j++)
                        {
                            Excel.Range range = worksheet.Cells[i, j] as Excel.Range;
                            string rad = Convert.ToString(range.Value2);
                            equipmentValues[j - 1] = range.Value2;
                        }

                        Barcode barcode = new Barcode
                        {
                            BarcodeValue = Convert.ToInt32(equipmentValues[6])
                        };

                        ContextConnector.db.Barcode.Add(barcode);
                        ContextConnector.db.SaveChanges();

                        Equipment newEquipment = new Equipment
                        {
                            Name = equipmentValues[0].ToString(),
                            Quantity = Convert.ToInt32(equipmentValues[1]),
                            SerialNumber = Convert.ToInt32(equipmentValues[2]),
                            StatusId = Convert.ToInt32(equipmentValues[3]),
                            Сharacteristic = equipmentValues[4].ToString(),
                            CategoryId = Convert.ToInt32(equipmentValues[5]),
                            BarcodeId = barcode.Id
                        };
                        ContextConnector.db.Equipment.Add(newEquipment);
                        MessageBox.Show("Вся оргтехника успешно добавлена", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                });
            }
        }

        public ImageSource ImageSource { get { return imageSource; } set { imageSource = value; OnPropertyChanged("ImageSource"); } }
        public ImageSource imageSource;

        public RelayCommand AddImageToEquipmentCommand
        {
            get
            {
                return new RelayCommand(obj => SetImageFromDialog());
            }
        }

        private void SetImageFromDialog()
        {
            OpenFileDialog imageDialog = new OpenFileDialog();
            imageDialog.Filter = "Изображения | *.png;*.jpeg;*jpg;";
            imageDialog.Multiselect = false;
            imageDialog.ShowDialog();

            if (!String.IsNullOrEmpty(imageDialog.FileName))
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(imageDialog.FileName);
                image.EndInit();
                ImageSource = image;
                ImagePath = imageDialog.FileName;
            }
        }

        public string EquipmentName { get { return equipmentName; } set { equipmentName = value; OnPropertyChanged("EquipmentName"); } }
        private string equipmentName;
        public string EquipmentQuantity { get { return equipmentQuantity; } set { equipmentQuantity = value; OnPropertyChanged("EquipmentQuantity"); } }
        private string equipmentQuantity;
        public string ImagePath { get { return imagePath; } set { imagePath = value; OnPropertyChanged("ImagePath"); } }
        public string imagePath;
        public string EquipmentSerialNumber { get { return equipmentSerialNumber; } set { equipmentSerialNumber = value; OnPropertyChanged("EquipmentSerialNumber"); } }
        private string equipmentSerialNumber;
        public string EquipmentCharacteristics { get { return equipmentCharacteristics; } set { equipmentCharacteristics = value; OnPropertyChanged("EquipmentCharacteristics"); } }
        private string equipmentCharacteristics;
        public EquipmentCategory EquipmentSelectedCategory { get { return equipmentSelectedCategory; } set { equipmentSelectedCategory = value; OnPropertyChanged("EquipmentSelectedCategory"); } }
        private EquipmentCategory equipmentSelectedCategory;

        private int[] serialNumbers;
        int[] BarCodeGenerate()
        {
            serialNumbers = new int[13];
            Random rand = new Random();
            for (int i = 0; i < serialNumbers.Length; i++)
            {
                serialNumbers[i] = rand.Next(2, 10);
            }
            return serialNumbers;
        }

        public string SerialNumbers
        {
            get
            {
                string numbers = String.Empty;
                for (int i = 0; i < serialNumbers.Length; i++)
                {
                    numbers += serialNumbers[i].ToString();
                }
                return numbers;
            }
        }
        private List<Line> linez = new List<Line>();

        public List<Line> GetBarcodeLines
        {
            get
            {
                linez.Clear();
                BarCodeGenerate();
                for (int i = 0; i < serialNumbers.Length; i++)
                {
                    Line barCodeLine = new Line
                    {
                        X2 = 0,
                        Y2 = 100,
                        Stroke = Brushes.Black,
                        StrokeThickness = serialNumbers[i] / 2,
                        HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch,
                        VerticalAlignment = VerticalAlignment.Stretch,
                        Margin = new Thickness(0, 0, 5, 0)
                    };
                    linez.Add(barCodeLine);

                }
                return linez;
            }
        }

        public RelayCommand AddEquipmentCommand
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    long barcode;
                    string barcodeString = "";
                    for (int i = 0; i < serialNumbers.Length; i++)
                    {
                        barcodeString += serialNumbers[i];
                    }
                    barcode = long.Parse(barcodeString);

                    Barcode newBarcode = new Barcode
                    {
                        BarcodeValue = barcode
                    };
                    ContextConnector.db.Barcode.Add(newBarcode);
                    ContextConnector.db.SaveChanges();

                    byte[] imageBytes = null;
                    if (ImagePath != null) imageBytes = Encoding.GetEncoding(1251).GetBytes(ImagePath);

                    Equipment newEquipment = new Equipment
                    {
                        Name = EquipmentName,
                        Quantity = int.Parse(EquipmentQuantity),
                        ImagePath = imageBytes,
                        SerialNumber = int.Parse(EquipmentSerialNumber),
                        StatusId = 1,
                        Сharacteristic = EquipmentCharacteristics,
                        CategoryId = EquipmentSelectedCategory.Id,
                        BarcodeId = newBarcode.Id
                    };
                    ContextConnector.db.Equipment.Add(newEquipment);
                    ContextConnector.db.SaveChanges();
                    MessageBox.Show("Оргтехника успешно добавлена", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    Frames.MainFrame.GoBack();
                });
            }
        }

        public RelayCommand ChangeColorCommand
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    ColorDialog dialog = new ColorDialog()
                    {
                        AnyColor = true,
                        AllowFullOpen = true
                    };
                    if (dialog.ShowDialog() != DialogResult.Cancel)
                    {
                        System.Drawing.Color color = dialog.Color;

                        Settings.Default.BackgroundColor = color;
                        Settings.Default.Save();
                        Settings.Default.Reload();
                        Settings.Default.Upgrade();
                    }
                });
            }
        }

        public Array DiagramTypes { get { return Enum.GetValues(typeof(SeriesChartType)); } }

        public SeriesChartType SelectedSeries { get { return selectedSeries; } set { selectedSeries = value; OnPropertyChanged("SelectedSeries"); } }
        private SeriesChartType selectedSeries;

        public string BookerName { get { return bookerName; } set { bookerName = value; OnPropertyChanged("BookerName"); } }
        private string bookerName;

        public string BookerLogin { get { return bookerLogin; } set { bookerLogin = value; OnPropertyChanged("BookerLogin"); } }
        private string bookerLogin;

        public string BookerPassword { get { return bookerPassword; } set { bookerPassword = value; OnPropertyChanged("BookerPassword"); } }
        private string bookerPassword;

        public string BookerEmail { get { return bookerEmail; } set { bookerEmail = value; OnPropertyChanged("BookerEmail"); } }
        private string bookerEmail;

        public string BookerPhone { get { return bookerPhone; } set { bookerPhone = value; OnPropertyChanged("BookerPhone"); } }
        private string bookerPhone;

        public RelayCommand BookerSetImageCommand
        {
            get
            {
                return new RelayCommand(obj => SetImageFromDialog());
            }
        }

        public RelayCommand RegisterCommand
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    if (Users.All(x => x.Login != BookerLogin))
                    {
                        byte[] imagePath;
                        if (!String.IsNullOrEmpty(ImagePath))
                        {
                            imagePath = Encoding.ASCII.GetBytes(ImagePath);
                        }
                        else imagePath = null;

                        User newUser = new User
                        {
                            FullName = BookerName,
                            Login = BookerLogin,
                            Password = BookerPassword,
                            RoleId = 2,
                            ImagePath = imagePath
                        };

                        ContextConnector.db.User.Add(newUser);
                        ContextConnector.db.SaveChanges();

                        Booker newBooker = new Booker
                        {
                            Id = newUser.Id,
                            Email = BookerEmail,
                            Phone = BookerPhone
                        };

                        ContextConnector.db.Booker.Add(newBooker);
                        ContextConnector.db.SaveChanges();
                        MessageBox.Show("Вы успешно зарегистрировались!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                        Frames.MainFrame.GoBack();
                    }
                });
            }
        }

        public RelayCommand GoBackCommand { get { return new RelayCommand(obj => Frames.MainFrame.GoBack()); } }
        public RelayCommand GoBackCommandWelcomeFrame { get { return new RelayCommand(obj => Frames.MainFrame.Navigate(new View.MainResourses.WelcomePage())); } }

        public RelayCommand DeleteStatusCommand { get { return new RelayCommand(obj => { EquipmentStatuses.Remove(SelectedStatus); ContextConnector.db.SaveChanges(); }); } }

        public RelayCommand AddStatusCommand { get { return new RelayCommand(obj => {
            string newStatusName = Interaction.InputBox("Введите статус");

            if (!String.IsNullOrEmpty(newStatusName))
            {
                EquipmentStatus newStatus = new EquipmentStatus
                {
                    Status = newStatusName
                };
                ContextConnector.db.EquipmentStatus.Add(newStatus);
                ContextConnector.db.SaveChanges();
                MessageBox.Show("Статус успешно добавлен", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        });
            }
        }

        public RelayCommand ImagePathTextBoxCommand { get { return new RelayCommand(obj =>
             {
                 if (obj != null)
                 {
                     try
                     {
                         GetImageSource(obj as String);
                     }
                     catch (UriFormatException)
                     {
                         Mb.Show("Не удалось загрузить картинку", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                         ImageSource = null;
                     }
                 }
             }
              );
            }
        }

        public RelayCommand CheckValueOnlyDigits
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    string value = obj as String;
                    char[] valueCharArr = value.ToArray();
                    for (int i = 0; i < valueCharArr.Length; i++)
                    {
                        if (!Char.IsDigit(valueCharArr[i]))
                        {
                            char[] ass = value.Where(x => x != valueCharArr[i]).ToArray();
                            string newvalue = null;
                            for (int j = 0; j < ass.Length; j++)
                            {
                                newvalue += ass[j];
                            }
                            EquipmentQuantity = newvalue;
                        }
                    }
                });
            }
        }

        private void GetImageSource(string path)
        {
            if (!String.IsNullOrEmpty(path))
            {
                ImageSource = new BitmapImage(new Uri(path));
            }
        }

        private int StatusNumber { get
            {
                for (int i = 0; i < EquipmentStatuses.Count; i++)
                {
                    if (selectedEquipment.StatusId == EquipmentStatuses[i].Id)
                    {
                        return i;
                    }
                }
                return 0;
            }
        }

        private List<DependencyObject> controls = new List<DependencyObject>();
        public List<DependencyObject> GetStatusVisuals { get
            {
                for (int i = 0; i <= StatusNumber; i++)
                {

                    Ellipse ellipse = new Ellipse
                    {
                        Fill = Brushes.Blue,
                        Width = 80,
                        Height = 80,
                        Margin = new Thickness(0, 0, 30, 0)
                    };

                    TextBlock block = new TextBlock
                    {
                        Text = EquipmentStatuses[i].Status
                    };


                    block.VerticalAlignment = VerticalAlignment.Center;
                    block.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                    block.Margin = new Thickness(-30, 0, 0, 0);
                    Grid grid = new Grid();
                    grid.Children.Add(ellipse);
                    grid.Children.Add(block);
                    controls.Add(grid);

                    if (i == StatusNumber)
                    {
                        ellipse.Fill = Brushes.Red;
                    }
                }
                return controls;
            }
            set { controls = value; OnPropertyChanged("GetStatusVisuals"); }
        }

        private List<Line> lines = new List<Line>();

        public List<Line> EquipmentLines { get {
                return GetEquipmentLinez(SelectedEquipment.Barcode.BarcodeValue);
            } 
        }

        private List<Line> GetEquipmentLinez(long barcode)
        {
            List<Line> lines = new List<Line>();
            long assad = barcode;

            int[] serialNumbers = assad.ToString().Select(a => int.Parse(a.ToString())).ToArray();

            for (int i = 0; i < serialNumbers.Length; i++)
            {
                Line barCodeLine = new Line
                {
                    X2 = 0,
                    Y2 = 100,
                    Stroke = Brushes.Black,
                    StrokeThickness = serialNumbers[i] / 2,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Stretch,
                    Margin = new Thickness(0, 0, 5, 0)
                };
                lines.Add(barCodeLine);

            }
            return lines;
        }
        public Visibility IsAdmin { get { if (CurrentUser.RoleId == 1) return Visibility.Visible; return Visibility.Hidden; } }

        public Visibility IsBooker { get { if (CurrentUser.RoleId == 2) return Visibility.Visible; return Visibility.Hidden; } }

        public RelayCommand EquipmentQuantityPlusCommand { get { return new RelayCommand(obj => { _SelectedEquipment.Quantity++; }); } }
        public RelayCommand EquipmentQuantityMinusCommand { get { return new RelayCommand(obj => { _SelectedEquipment.Quantity--; }); } }


        public RelayCommand ChangeStatusCommand { get { return new RelayCommand(obj => {
            SelectedEquipment.EquipmentStatus = SelectedStatus;
        }); 
        }
        }

        public RelayCommand ExcelExportCommand { get { return new RelayCommand(obj => {
            var application = new Word.Application();
            Word.Document document = application.Documents.Add();
            Word.Paragraph paragraph = document.Paragraphs.Add();
            Word.Range range = paragraph.Range;
            range.InsertParagraphAfter();
            int rows = Equipment.Count+1;
            int columns = 7;
            Word.Table table = document.Tables.Add(range,rows,columns);
            table.Borders.InsideLineStyle = table.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
            table.Range.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

            Word.Range cellRange;

            cellRange = table.Cell(1, 1).Range;
            cellRange.Text = "ID";

            cellRange = table.Cell(1, 2).Range;
            cellRange.Text = "Количество";

            cellRange = table.Cell(1, 3).Range;
            cellRange.Text = "Серийный номер";

            cellRange = table.Cell(1, 4).Range;
            cellRange.Text = "Статус";

            cellRange = table.Cell(1, 5).Range;
            cellRange.Text = "Характеристики";

            cellRange = table.Cell(1, 6).Range;
            cellRange.Text = "Категория";

            cellRange = table.Cell(1, 7).Range;
            cellRange.Text = "Штрихкод";

            int i = 2;
            for (int j = 0; j < Equipment.Count; j++)
            {
                cellRange = table.Cell(i, 1).Range;
                cellRange.Text = Equipment[j].Id.ToString();

                cellRange = table.Cell(i, 2).Range;
                cellRange.Text = Equipment[j].Quantity.ToString();

                cellRange = table.Cell(i, 3).Range;
                cellRange.Text = Equipment[j].SerialNumber.ToString();

                cellRange = table.Cell(i, 4).Range;
                cellRange.Text = Equipment[j].EquipmentStatus.Status;

                cellRange = table.Cell(i, 5).Range;
                cellRange.Text = Equipment[j].Сharacteristic;

                cellRange = table.Cell(i, 6).Range;
                cellRange.Text = Equipment[j].EquipmentCategory.Name;

                cellRange = table.Cell(i, 7).Range;
                cellRange.Text = Equipment[j].Barcode.BarcodeValue.ToString();
                i++;
            }

            application.Visible = true;
        }); 
            } 
        }

        public RelayCommand OpenInstruction { get {
                return new RelayCommand(obj => {
                    Word.Application ap = new Word.Application();
                    var doc = ap.Documents.Open(Directory.GetCurrentDirectory() + @"\Instruction.docx");
                    ap.Visible = true;
                }
                );
            }
        }

        public RelayCommand OpenInformation => new RelayCommand(obj => {
            Frames.WelcomeFrame.Navigate(new View.MainResourses.InfoPage());
        }
        );

        public event PropertyChangedEventHandler PropertyChanged;
               
        public async void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
                await ContextConnector.db.SaveChangesAsync();
            }
        }
    }
}