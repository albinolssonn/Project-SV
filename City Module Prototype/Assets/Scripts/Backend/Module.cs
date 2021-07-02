using UnityEngine;


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
    public abstract int blockIndex();


    /// <returns>The height value of the module.</returns>
    public abstract int Height();


    /// <returns>The path where the resource for the visual model is located on the device.</returns>
    public abstract string GetResourcePath();
    //Should return: "Modules/MODELNAME" where MODELNAME is the filename of the resource.


    /// <returns>A new instance of the same object type.</returns>
    public abstract Module Copy();
}

//---------------------------------------------------------------------

public class Park : Module
{

    public override int blockIndex()
    {
        return 0;
    }

    public override int Height()
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
}

//---------------------------------------------------------------------

public class House : Module
{

    public override int blockIndex()
    {
        return 1;
    }

    public override int Height()
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
}

//---------------------------------------------------------------------

public class TallBuilding : Module
{

    public override int blockIndex()
    {
        return 2;
    }

    public override int Height()
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
}

//---------------------------------------------------------------------

public class Hospital : Module
{

    public override int blockIndex()
    {
        return 2;
    }

    public override int Height()
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
}

//---------------------------------------------------------------------

public class PoliceStation: Module
{

    public override int blockIndex()
    {
        return 1;
    }

    public override int Height()
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
}

//---------------------------------------------------------------------

public class FireDepartment : Module
{

    public override int blockIndex()
    {
        return 1;
    }

    public override int Height()
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
}

//---------------------------------------------------------------------

public class Antenna : Module
{

    public override int blockIndex()
    {
        return 0;
    }

    public override int Height()
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
}

//---------------------------------------------------------------------