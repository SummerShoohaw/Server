using System;

namespace Server.Models{
    public class Pet{
        public int ID { get; set; }
        public string name{get;set;}
        public int age{get;set;} // days of age, not years of age
        public int weight{get;set;}
        public int exer{get;set;}
        public int imageNum { get; set; }

        public Pet(){}

		public void Eat(int number){
			//not implemented yet
		}

        public static Pet NewPet(int petType,string petName){
            Pet pet = new Pet();
            pet.age = 0;
            pet.weight = 0;
            pet.exer = 0;
            pet.imageNum = petType;
            pet.name = petName;
            return pet;
        }
        
    }
}