using UnityEngine;

//HACK: In this file you can with ease create additional modules to place on the grid.
//For new module to be legal in the program, it has to inherit from the abstract class Module below
//and implement the required methods. Then it is just to add the newly created module to the grid
//as any other pre-existing module!


/// <summary>
/// Used to represent the different modules guests will be able to place upon a city tile.
/// </summary>
public abstract class Module
{

    /// <summary>
    /// The GameObject which is visualized on the screen for the user.
    /// </summary>
    public GameObject visualObject;


    /// <returns>The amount of which the module blocks the signal.</returns>
    public abstract double BlockIndex();


    /// <returns>The capacity demand of this module.</returns>
    public abstract double CapacityDemand();


    /// <returns>The height value of the module.</returns>
    public abstract double Height();


    /// <returns>The path where the resource for the visual model is located on the device.</returns>
    public abstract string GetResourcePath();
    //Should return: "Modules/MODELNAME" where MODELNAME is the filename of the resource.


    /// <returns>A new instance of the same object type.</returns>
    public abstract Module Copy();


    /// <returns>If this module is classed as critical infrastructure.</returns>
    public abstract bool IsCritical();
}

//---------------------------------------------------------------------

public class Park : Module
{

    public override double BlockIndex()
    {
        return 0;
    }

    public override double CapacityDemand()
    {
        return 3;
    }

    public override double Height()
    {
        return 0;
    }

    public override string GetResourcePath()
    {
        return "Modules/Park";
    }

    public override Module Copy()
    {
        return new Park();
    }

    public override bool IsCritical()
    {
        return false;
    }
}

//---------------------------------------------------------------------

public class House : Module
{

    public override double BlockIndex()
    {
        return 1;
    }

    public override double CapacityDemand()
    {
        return 2;
    }

    public override double Height()
    {
        return 1;
    }

    public override string GetResourcePath()
    {
        return "Modules/House";
    }

    public override Module Copy()
    {
        return new House();
    }

    public override bool IsCritical()
    {
        return false;
    }
}

//---------------------------------------------------------------------

public class TallBuilding : Module
{

    public override double BlockIndex()
    {
        return 2;
    }

    public override double CapacityDemand()
    {
        return 4;
    }

    public override double Height()
    {
        return 3;
    }

    public override string GetResourcePath()
    {
        return "Modules/TallBuilding";
    }

    public override Module Copy()
    {
        return new TallBuilding();
    }

    public override bool IsCritical()
    {
        return false;
    }
}

//---------------------------------------------------------------------

public class Hospital : Module
{

    public override double BlockIndex()
    {
        return 2;
    }

    public override double CapacityDemand()
    {
        return 4;
    }

    public override double Height()
    {
        return 2;
    }

    public override string GetResourcePath()
    {
        return "Modules/Hospital";
    }

    public override Module Copy()
    {
        return new Hospital();
    }

    public override bool IsCritical()
    {
        return true;
    }
}

//---------------------------------------------------------------------

public class PoliceStation : Module
{

    public override double BlockIndex()
    {
        return 1;
    }

    public override double CapacityDemand()
    {
        return 2;
    }

    public override double Height()
    {
        return 2;
    }

    public override string GetResourcePath()
    {
        return "Modules/PoliceStation";
    }

    public override Module Copy()
    {
        return new PoliceStation();
    }

    public override bool IsCritical()
    {
        return true;
    }
}

//---------------------------------------------------------------------

public class FireDepartment : Module
{

    public override double BlockIndex()
    {
        return 1;
    }

    public override double CapacityDemand()
    {
        return 2;
    }

    public override double Height()
    {
        return 2;
    }

    public override string GetResourcePath()
    {
        return "Modules/FireDepartment";
    }

    public override Module Copy()
    {
        return new FireDepartment();
    }

    public override bool IsCritical()
    {
        return true;
    }
}

//---------------------------------------------------------------------

public class Antenna : Module
{
    /// <summary>
    /// The current demand on this Antenna.
    /// </summary>
    private double currentCapacityDemand = 0;


    /// <summary>
    /// Adds the given value to the current demand of the Antenna.
    /// </summary>
    /// <param name="demand">The value to add.</param>
    public void AddDemand(double demand)
    {
        currentCapacityDemand += demand;
    }


    /// <summary>
    /// Removes the given value from the current demand of the Antenna.
    /// </summary>
    /// <param name="demand">Value to remove.</param>
    public void RemoveDemand(double demand)
    {
        currentCapacityDemand -= demand;
        if (currentCapacityDemand < 0)
        {
            throw new System.Exception("'currentDemand' less than 0.");
        }
    }


    /// <summary>
    /// Sets current demand back to 0.
    /// </summary>
    public void ResetDemand()
    {
        currentCapacityDemand = 0;
    }


    /// <returns>Current demand on this Antenna.</returns>
    public double GetDemand()
    {
        return currentCapacityDemand;
    }


    /// <returns>The available capacity of this antenna.</returns>
    public double AvailableCapacity()
    {
        return GridManager.baseCapacity - currentCapacityDemand;
    }


    public override double BlockIndex()
    {
        return 0;
    }

    public override double CapacityDemand()
    {
        return 0;
    }

    public override double Height()
    {
        return 0;
    }

    public override string GetResourcePath()
    {
        return "Modules/Antenna";
    }

    public override Module Copy()
    {
        return new Antenna();
    }

    public override bool IsCritical()
    {
        return false;
    }


}

//---------------------------------------------------------------------