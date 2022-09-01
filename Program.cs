Course course = new Course();
course.Title = "C Programming";
course.Fees = 7000;
course.Teacher = new Instructor()
{
    Name = "Fazlul Karim",
    Email = "karim023@gmail.com",
    PresentAddress = new Address()
    {
        Street = "Mirpur",
        City = "Dhaka",
        Country = "Bangladesh"
    },
    PermanentAddress = new Address()
    {
        Street = "Notullabad",
        City = "Barisal",
        Country = "Bangladesh"
    },

    PhoneNumbers = new List<Phone>
    {
        new Phone()
        {
            Number="1614643736",
            Extension="07",
            CountryCode="+880"
        },
        new Phone()
        {
            Number="1996507877",
            Extension="08",
            CountryCode="+880"
        }
    }
};

course.Topics = new List<Topic>()
{
    new Topic()
    {
        Title="DP",
        Description="Dynamic Programming",
        Sessions=new List<Session>()
        {
            new Session()
            {
                DurationInHour=30,
                LearningObjective="Competitive Programming"
            }
        }
    }
};

course.Tests = new List<AdmissionTest>()
{
    new AdmissionTest()
    {
        StartDateTime=new DateTime(2022,6,30,19,30,00),
        EndDateTime=new DateTime(2022,6,30,21,00,00),
        TestFees=300
    }
};

string JSON = JsonFormatter.Convert(course);
Console.WriteLine(JSON);







