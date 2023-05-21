public static class Mage1st
{
    public static ProjectileObject EnergyBolt()
    {
        var ob = new ProjectileObject();
        ob.setProj(ProjectileType.Tracking, true, true, 100f);
        return ob;
    }

    public static ProjectileObject FireBall()
    {
        var ob = new ProjectileObject();
        ob.setProj(ProjectileType.Directional, true, false, 100f);
        return ob;
    }

    public static ProjectileObject HolyArrow()
    {
        var ob = new ProjectileObject();
        ob.setProj(ProjectileType.Targeted, false, false, 100f);
        return ob;
    }
    
    public static ProjectileObject ColdBeam()
    {
        var ob = new ProjectileObject();
        ob.setProj(ProjectileType.Directional, false, true, 100f);
        return ob;
    }
}


