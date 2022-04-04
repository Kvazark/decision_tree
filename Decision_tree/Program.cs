using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;

namespace Decision_tree
{
    class Data
    {
        public string language { get; set; } //'en' or 'fr' or 'ch'
        public string education { get; set; }//'MD'(master's degree) or 'BD'(bachelor's degree) or 'GSE'(general special education)
        public string? workExperience { get; set; }//'yes' or 'no' (Опыт работы больше 3-х лет?)
        public string? badHabits { get; set; }//'yes' or 'no'
        public string? nativeSpeakers { get; set; }//'yes' or 'no'
        
    }
    delegate void DecisionDelegate(Data data);
    delegate DecisionDelegate Delegate4(Data data);
    delegate Delegate4 Delegate3(Data data);

    delegate Delegate3 Delegate2(Data data);
    delegate Delegate2 Delegate1(Data data);

    class Program
    {
        static void Main(string[] args)
        {
          
            
            void BadAnswer(Data data) => Console.WriteLine("Unfortunately, we have to refuse you...");
            void GoodAnswer(Data data) => Console.WriteLine("You are accepted!");
            
            var list = Menu();
            
            var information = new Data(){language=list.ElementAt(0), education= list.ElementAt(1), workExperience = list.ElementAt(2),  badHabits = list.ElementAt(4), nativeSpeakers = list.ElementAt(3)};

            Language(information)(information)(information)(information)(information)(information);

            Delegate1 Language(Data data) => data.language == "En" ? EduacationEn : data.language == "Fr" ? EducationFr : EducationCh;
            
            Delegate2 EduacationEn(Data data) => data.education == "GSE" ? WorkExperienceEn: WorkExperienceGood;
            Delegate2 EducationFr(Data data) => data.education == "MD"? WorkExperienceFrm: data.education =="BD"? WorkExperienceFrb: WorkExperienceBad;
            Delegate2 EducationCh(Data data) => data.education == "MD"? WorkExperienceCh: WorkExperienceBad;;

            Delegate3 WorkExperienceGood(Data data) => NativeSpeakersGood;
            Delegate3 WorkExperienceBad(Data data) => NativeSpeakersBad;

            Delegate3 WorkExperienceEn(Data data) => data.workExperience == "yes" ? NativeSpeakersGood: NativeSpeakersEn;

            Delegate3 WorkExperienceFrm(Data data) => data.workExperience == "yes" ? NativeSpeakersGood : NativeSpeakersFr;

            Delegate3 WorkExperienceFrb(Data data) => data.workExperience == "yes" ? NativeSpeakersFr : NativeSpeakersBad;

            Delegate3 WorkExperienceCh(Data data) => data.workExperience == "yes" ? NativeSpeakersCh : NativeSpeakersBad;
            
          
            Delegate4 NativeSpeakersGood(Data data) => BadHabitsGood;
            Delegate4 NativeSpeakersBad(Data data) => BadHabitsBad;
            
            Delegate4 NativeSpeakersEn(Data data) => data.nativeSpeakers =="yes"? BadHabitsGood: BadHabitsEn;
            Delegate4 NativeSpeakersFr(Data data) => data.nativeSpeakers == "yes" ? BadHabitsGood : data.nativeSpeakers == "no" ? BadHabitsBad: BadHabitsFr;
            Delegate4 NativeSpeakersCh(Data data) => data.nativeSpeakers == "yes" ? BadHabitsCh : BadHabitsBad;
            
            
            DecisionDelegate BadHabitsGood(Data data) => GoodAnswer;
            DecisionDelegate BadHabitsBad(Data data) => BadAnswer;
            
            
            DecisionDelegate BadHabitsEn(Data data) => data.badHabits == "no" ? GoodAnswer : BadAnswer;
            DecisionDelegate BadHabitsFr(Data data) => data.badHabits == "no" ? GoodAnswer : BadAnswer;
            DecisionDelegate BadHabitsCh(Data data) => data.badHabits == "no" ? GoodAnswer : BadAnswer;

        }

        public static List<string> Menu()
        {
            var list = new List<string>();
            string error = "Некорректный ввод. Введите значение снова: ";
            Console.Write("Какой язык вы знаете?\nВведите 'En', если это английский, 'Fr' - французский, 'Ch'- китайский: ");
            string input = Console.ReadLine();
            while (input != "En" & input != "Fr" & input != "Ch")
            {
                Console.WriteLine(error);
                input = Console.ReadLine();
            }
            list.Add(input);
            Console.Write("Какое у вас образование?\nВведите 'MD', если вы успешно закончили магистратуру, 'BD' - бакалавриат, 'GSE'- среднее специалльное: ");
            input = Console.ReadLine();
            while (input != "MD" & input != "BD" & input != "GSE")
            {
                Console.WriteLine(error);
                input = Console.ReadLine();
            }
            list.Add(input);
            Console.WriteLine("Какой  у вас опыт работы.\nНажмите Enter для пропуска или ведите число(в годах): ");
            string? input2 = Console.ReadLine();
            if (input2!=null)
            {
                if (int.TryParse(input2, out int number))
                {
                    int num = Convert.ToInt32(input2);
                    if (num >= 3 & num <=45)
                    {
                        list.Add("yes");
                    }
                    else if (num < 3 & num >=0)
                    {
                        list.Add("no");
                    }
                }
                else
                {
                    list.Add(null);
                }
            }
            else
            {
                list.Add(null);
            }
            Console.WriteLine("Являетесь ли вы носителем языка?\nНажмите Enter для пропуска или введите 'yes' если да, 'no'- если нет: ");
            string? input3 = Console.ReadLine();
            if (input3 == "yes")
            {
                list.Add("yes");
            }
            else if (input3 == "no")
            {
                list.Add("no");
            }
            else
            {
                list.Add(null);
            }
            Console.WriteLine("У вас есть вредные привычки?\nНажмите Enter для пропуска или введите 'yes' если да, 'no'- если нет: ");
            input3 = Console.ReadLine();
            if (input3 == "yes")
            {
                list.Add("yes");
            }
            else if (input3 == "no")
            {
                list.Add("no");
            }
            else
            {
                list.Add(null);
            }
            
            return list;
        }
    }
}