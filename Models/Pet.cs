using System;

namespace Server.Models{
    public abstract class Pet{
        public string namet{get;set;}
        public int age{get;set;} // days of age, not years of age
        public int weight{get;set;}
        public int hungry{get;set;}
        public bool returned{get;set;} = false;

        
    }
}