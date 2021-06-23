using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Module
{
    public abstract int modifier();
    public abstract int height();
    public abstract string GetResourcePath(); 

}

public class Park : Module
{
    public override int modifier()
    {
        return 0;
    }

    public override int height()
    {
        return 0; 
    }

    public override string GetResourcePath()
    {
        return "Resources/Park"; 
    }
}

public class House : Module
{
    public override int modifier()
    {
        return -1;
    }

    public override int height()
    {
        return 1;
    }

    public override string GetResourcePath()
    {
        return "Resources/House";
    }
}

public class TallBuilding : Module
{
    public override int modifier()
    {
        return -2;
    }

    public override int height()
    {
        return 3;
    }

    public override string GetResourcePath()
    {
        return "Resources/TallBuilding";
    }
}

public class Hospital : Module
{
    public override int modifier()
    {
        return -2;
    }

    public override int height()
    {
        return 2;
    }

    public override string GetResourcePath()
    {
        return "Resources/Hospital";
    }
}

public class PoliceStation: Module
{
    public override int modifier()
    {
        return -1;
    }

    public override int height()
    {
        return 2;
    }

    public override string GetResourcePath()
    {
        return "Resources/PoliceStation";
    }
}

public class FireDepartment : Module
{
    public override int modifier()
    {
        return -1;
    }

    public override int height()
    {
        return 2;
    }

    public override string GetResourcePath()
    {
        return "Resources/FireDepartment";
    }
}

public class Antenna : Module
{
    public override int modifier()
    {
        return 0;
    }

    public override int height()
    {
        return 0;
    }

    public override string GetResourcePath()
    {
        return "Modules/Antenna";
    }
}

