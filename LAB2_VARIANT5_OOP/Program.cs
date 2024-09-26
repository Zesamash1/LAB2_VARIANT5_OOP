using System;

namespace LAB2_VARIANT5_OOP

{
    // Клас для представлення фільму
    public class Movie
    {
        // Властивості 
        public string Title { get; set; } // Назва фільму
        public string Director { get; set; }  // Режисер фільму
        public int ReleaseYear { get; private set; }  // Рік випуску (тільки для читання)
        public int Duration { get; private set; } // Тривалість фільму (тільки для читання)
        private int currentTime; // Змінна для зберігання поточного часу перегляду

        // Властивість для роботи з поточним часом перегляду фільму
        public int CurrentTime
        {
            get { return currentTime; }
            set
            {
                // Перевірка, що час не менший за 0
                if (value < 0)
                {
                    Console.WriteLine("Помилка: поточний час не може бути менше 0.");
                    currentTime = 0;
                }
                // Перевірка, що час не більший за тривалість фільму
                else if (value > Duration)
                {
                    Console.WriteLine("Помилка: поточний час не може бути більше за тривалість фільму.");
                }
                else
                {
                    currentTime = value; // Якщо все в порядку, зберігаємо новий поточний час
                }
            }
        }

        // Конструктор за замовчуванням
        public Movie()
        {
            Title = string.Empty;  // Порожня назва
            Director = string.Empty;  // Порожнє ім'я режисера
            ReleaseYear = 0;  // Рік за замовчуванням
            Duration = 0;  // Тривалість за замовчуванням
            currentTime = 0;  // Початковий поточний час 0
        }

        // Конструктор з параметрами
        public Movie(string title, string director, int releaseYear, int duration)
        {
            Title = title;
            Director = director;
            ReleaseYear = releaseYear;
            Duration = duration;
            currentTime = 0;  // Початковий час 0
        }

        // Конструктор копіювання
        public Movie(Movie other)
        {
            Title = other.Title;  // Копіюємо назву
            Director = other.Director;  // Копіюємо режисера
            ReleaseYear = other.ReleaseYear;  // Копіюємо рік випуску
            Duration = other.Duration;  // Копіюємо тривалість
            currentTime = other.currentTime;  // Копіюємо поточний час
        }

         // Метод для відтворення фільму протягом певної кількості хвилин
        public void Play(int minutes)
        {
            // Перевірка на негативні хвилини
            if (minutes < 0)
            {
                Console.WriteLine("Помилка: кількість хвилин для перегляду не може бути від'ємною.");
                return;
            }

            // Перевірка, чи не перевищує час тривалість фільму
            if (currentTime + minutes > Duration)
            {
                Console.WriteLine("Помилка: перегляд перевищив тривалість фільму. Зміни не прийняті.");
            }
            else
            {
                currentTime += minutes;  // Додаємо хвилини до поточного часу
                Console.WriteLine($"Переглянуто {minutes} хвилин. Поточний час: {currentTime} хвилин.");
            }
        }

        // Метод для отримання відсотка перегляду фільму
        public double GetProgress()
        {
            if (Duration == 0) return 0;  // Якщо тривалість 0, повертаємо 0%
            return (double)currentTime / Duration * 100;  // Обчислюємо відсоток перегляду
        }

        // Метод для виведення інформації про фільм
        public void PrintInfo()
        {
            Console.WriteLine($"Назва: {Title}");
            Console.WriteLine($"Режисер: {Director}");
            Console.WriteLine($"Рік випуску: {ReleaseYear}");
            Console.WriteLine($"Тривалість: {Duration} хвилин");
            Console.WriteLine($"Переглянуто: {GetProgress():0.00}%");
        }

        // Метод для клонування фільму
        public Movie Clone()
        {
            return new Movie(this);  // Повертаємо копію поточного фільму
        }

        // Оператор для порівняння фільмів
        public static bool operator ==(Movie m1, Movie m2)
        {
            if (ReferenceEquals(m1, m2)) return true;  // Якщо це один і той самий об'єкт
            if (ReferenceEquals(m1, null) || ReferenceEquals(m2, null)) return false;  // Якщо один з фільмів null
            return m1.Title == m2.Title && m1.Director == m2.Director;  // Порівнюємо за назвою і режисером
        }

        // Оператор для порівняння фільмів на нерівність
        public static bool operator !=(Movie m1, Movie m2)
        {
            return !(m1 == m2);
        }

        // Перевизначений метод Equals для порівняння об'єктів
        public override bool Equals(object obj)
        {
            if (obj is Movie other)
            {
                return this == other;  // Використовуємо перевизначений оператор
            }
            return false;
        }
        // Хеш-код для підвищення ефективності порівняння об'єктів
        public override int GetHashCode()
        {
            return Title.GetHashCode() ^ Director.GetHashCode();  // Хеш-код на основі назви і режисера
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Movie currentMovie = null;  // Поточний фільм
            bool exit = false;  // Змінна для завершення програми
            Console.OutputEncoding = System.Text.Encoding.UTF8;  // Це для того щоб замість "?" було видно літеру "і"

            while (!exit)
            {
                Console.Clear();  // Очищення консолі
                Console.ForegroundColor = ConsoleColor.Yellow;  // Зробити текст меню з командами жовтого кольору

                // Виведення діалогового вікна з користувачем
                Console.WriteLine("=== Media ===");
                Console.WriteLine("1. Створити новий фільм");
                Console.WriteLine("2. Показати інформацію про фільм");
                Console.WriteLine("3. Змінити назву фільму");
                Console.WriteLine("4. Змінити режисера фільму");
                Console.WriteLine("5. Встановити поточний час перегляду");
                Console.WriteLine("6. Переглянути фільм певний час (додати до поточного)");
                Console.WriteLine("7. Показати відсоток перегляду фільму");
                Console.WriteLine("8. Копіювати фільм");
                Console.WriteLine("9. Порівняти два фільми");
                Console.WriteLine("10. Вийти");
                Console.WriteLine("==========================");

                Console.ForegroundColor = ConsoleColor.White;  // Повернення стандартного кольору

                Console.Write("Введіть номер команди: ");  // Запит на введення команди
                switch (Console.ReadLine())
                {
                    case "1":
                        // Створення нового фільму
                        Console.Write("Введіть назву фільму: ");
                        string title = Console.ReadLine();
                        Console.Write("Введіть режисера фільму: ");
                        string director = Console.ReadLine();
                        Console.Write("Введіть рік випуску: ");
                        int releaseYear = int.Parse(Console.ReadLine());
                        Console.Write("Введіть тривалість фільму у хвилинах: ");
                        int duration = int.Parse(Console.ReadLine());
                        currentMovie = new Movie(title, director, releaseYear, duration);
                        Console.WriteLine("Фільм створено!\n");
                        break;

                    case "2":
                        // Показати інформацію про фільм
                        if (currentMovie != null)
                        {
                            currentMovie.PrintInfo();
                        }
                        else
                        {
                            Console.WriteLine("Фільм не створено!\n");
                        }
                        break;

                    case "3":
                        // Змінити назву фільму
                        if (currentMovie != null)
                        {
                            Console.Write("Введіть нову назву фільму: ");
                            currentMovie.Title = Console.ReadLine();
                            Console.WriteLine("Назву змінено!\n");
                        }
                        else
                        {
                            Console.WriteLine("Фільм не створено!\n");
                        }
                        break;

                    case "4":
                        // Змінити режисера фільму
                        if (currentMovie != null)
                        {
                            Console.Write("Введіть нового режисера фільму: ");
                            currentMovie.Director = Console.ReadLine();
                            Console.WriteLine("Режисера змінено!\n");
                        }
                        else
                        {
                            Console.WriteLine("Фільм не створено!\n");
                        }
                        break;

                    case "5":
                        // Встановити поточний час перегляду
                        if (currentMovie != null)
                        {
                            Console.Write("Введіть новий поточний час перегляду у хвилинах: ");
                            int newTime = int.Parse(Console.ReadLine());
                            currentMovie.CurrentTime = newTime;
                            Console.WriteLine("Поточний час перегляду встановлено!\n");
                        }
                        else
                        {
                            Console.WriteLine("Фільм не створено!\n");
                        }
                        break;

                    case "6":
                        // Переглянути фільм певний час (додати до поточного)
                        if (currentMovie != null)
                        {
                            Console.Write("На скільки хвилин додати перегляду: ");
                            int minutes = int.Parse(Console.ReadLine());
                            currentMovie.Play(minutes);
                            Console.WriteLine("Перегляд додано!\n");
                        }
                        else
                        {
                            Console.WriteLine("Фільм не створено!\n");
                        }
                        break;

                    case "7":
                        // Показати відсоток перегляду фільму
                        if (currentMovie != null)
                        {
                            Console.WriteLine($"Відсоток перегляду фільму: {currentMovie.GetProgress():0.00}%\n");
                        }
                        else
                        {
                            Console.WriteLine("Фільм не створено!\n");
                        }
                        break;

                    case "8":
                        // Копіювати фільм
                        if (currentMovie != null)
                        {
                            Movie clonedMovie = currentMovie.Clone();
                            Console.WriteLine("Фільм скопійовано! Ось інформація про скопійований фільм:");
                            clonedMovie.PrintInfo();
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine("Фільм не створено!\n");
                        }
                        break;

                    case "9":
                        // Порівняти два фільми
                        if (currentMovie != null)
                        {
                            Console.WriteLine("Порівняння з іншим фільмом.");
                            Console.Write("Введіть назву іншого фільму: ");
                            string otherTitle = Console.ReadLine();
                            Console.Write("Введіть режисера іншого фільму: ");
                            string otherDirector = Console.ReadLine();
                            Movie otherMovie = new Movie(otherTitle, otherDirector, 0, 0);
                            if (currentMovie == otherMovie)
                            {
                                Console.WriteLine("Фільми однакові!\n");
                            }
                            else
                            {
                                Console.WriteLine("Фільми різні!\n");
                            }
                        }
                        else
                        {

                            Console.WriteLine("Фільм не створено!\n");
                        }
                        break;

                    case "10":
                        // Завершення роботи програми
                        exit = true;
                        break;

                    default:
                        // Помилка вибору(Введено не те значення або нічого)
                        Console.WriteLine("Неправильна команда!\n");
                        break;
                }

                if (!exit)
                {
                    Console.WriteLine("Натисніть будь-яку клавішу, щоб продовжити...");
                    Console.ReadKey();  // Очікуємо натискання клавіші для продовження
                }
            }
        }
    }
}
