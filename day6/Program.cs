﻿// Non -Generic 

// boxing/unboxing
// boxing :- When we convert a value type(like int ) into an object type(reference type)

// int num value1 = 10;
// object boxedvalue = num;  // boxing

// Unboxing  :-  when we extract the value type from an object

// int valuetype = (int)boxedvalue;  // unboxing

// These operations are expensive in performance :- then there will be a memory overhead  ,
//   This one is non type-safe because by mistaken wrong casting can be done which may cause a run time errors

//   List<Student> list = new List<Student>();

//   for(int i=0 ; i<5 ; i++)
//   {
//     Student s = new Student(3434,"df");
//      list.Add(s);
//   }

//   List<int> numbers = new List<int>();
//   numbers.Add(10);  // No boxing
//   int result = numbers[0]; // No Unboxing
 
//  // Non-Generic
//   List numbers = new List();
//   numbers.Add(10);  // No boxing
//   int result = numbers[0]; // No Unboxing

//   c# provides some wrapper classes 

//   Wrapper class is a class that wraps value types to provide some extra functionalities
//   System.Object which is the universal 

//   System.Int32 -- wraps -- value type which is int  -- Generic class for type safety at compile time only you are telling to compiler that internally should convert into int
//   System.Double -- wraps -- value type which is Double
//   Whenever we are boxing to an int ,  it is converting it into System.Object but internally it uses System.Int32;


// // See https://aka.ms/new-console-template for more information
// using System.Collections;
// class NonGenericExample
// {

//     static void Main()
//     {
        // int num = 45;
        // object boxedvalue = num; //Boxing

        // Console.WriteLine(boxedvalue.GetType());

        // ArrayList arrayList = new ArrayList();  // It will store the values in object
        // int value = 20;

        // arrayList.Add(value); // Boxing
        // Console.WriteLine(arrayList.GetType());

        // double unboxingvalue = (double)arrayList[0]; // Unboxing

        // Console.WriteLine("The value after unboxing " + unboxingvalue);

        // //Generic one
        // List<int> intList = new List<int>();
        // intList.Add(value); // No Boxing is required
        //  double value2 = intList[0]; // No unBoxing is required
        //   Console.WriteLine("The value after unboxing " + value2);
        // ArrayList list = new ArrayList();
        // list.add(56);
       
        
  //  }


//}


