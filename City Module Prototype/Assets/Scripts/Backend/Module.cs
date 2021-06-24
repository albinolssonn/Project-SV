using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Module
{
    public GameObject visualObject;

    public abstract int Modifier();
    public abstract int Height();
    public abstract string GetResourcePath();
    public abstract Module Copy();
}

public class Park : Module
{

    public override int Modifier()
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

public class House : Module
{

    public override int Modifier()
    {
        return -1;
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

public class TallBuilding : Module
{

    public override int Modifier()
    {
        return -2;
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

public class Hospital : Module
{

    public override int Modifier()
    {
        return -2;
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

public class PoliceStation: Module
{

    public override int Modifier()
    {
        return -1;
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

public class FireDepartment : Module
{

    public override int Modifier()
    {
        return -1;
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

public class Antenna : Module
{

    public override int Modifier()
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

