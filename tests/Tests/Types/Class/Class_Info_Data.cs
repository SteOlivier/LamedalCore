using LamedalCore.domain.Attributes;
using LamedalCore.domain.Enumerals;

namespace LamedalCore.Test.Tests.Types.Class
{
    public enum enDogType
    {
        Unknown, Bulldog
    }

    public enum enSpecies
    {
        Unknown, Dog
    }

    [BlueprintRule_Class(enBlueprintClassNetworkType.Node_Data)]
    [BlueprintData_Table("This is animal data")]
    public class Types_ClassInfo_Animal 
    { 
        // Fields
        [BlueprintData_Field("The legs:")]
        public int Legs = -1;

        [BlueprintData_Field("The age:")]
        public int Age = -1;

        public int Health = 100;

        // Properties
        [BlueprintData_Field("The species:")]
        public enSpecies Species
        {
            get { return _Species;}
            set { _Species = value;}
        }
        private enSpecies _Species = enSpecies.Unknown;

        // Methods
        public int BirhthDay()
        {
            Age++;
            return Age;
        }

        public void Health_Set(int newHealth)
        {
            Health = newHealth;
        }
    } 

    public class Types_ClassInfo_Dog : Types_ClassInfo_Animal
    {
        // Fields
        public enDogType DogType = enDogType.Unknown;

        // Properties
        public string OwnderName
        {
            get { return _ownderName; }
            set { _ownderName = value; }
        }
        private string _ownderName = "";

        // Constructor
        public Types_ClassInfo_Dog(int age)
        {
            Legs = 4;
            Species = enSpecies.Dog;
            Age = age;
        }
    }

    public sealed class Types_ClassInfo_Dog_Bulldog : Types_ClassInfo_Dog
    {
        // Constructor
        public Types_ClassInfo_Dog_Bulldog(int age) : base(age)
        {
            DogType = enDogType.Bulldog;
        }
    }
}
