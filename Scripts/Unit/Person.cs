using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : AbstractAttributable<PersonAttributes>{

    public string Name;

    public override Dictionary<string, int> GetAttributes(){
        return Attributes.ToDictionary();
    }

    void Start(){
        
    }

    void InitializeAttributes(int attributePoints){
        //This method needs to randomly assign the attribute points between all attributes
        //Consider a For loop that goes through each attribute and assigns +1 to it if random = true, and +0 if false
        //May be computationally tedious

        
        //this.Attributes = new PersonAttributes(PARAMETERS);
        
    }

}
