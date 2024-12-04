using System;
using System.Linq;
using System.Collections.Generic;

public abstract class Attraction
{
	public Attraction(string name, int capacity)
	{
		this.name = name;
		this.capacity = capacity;
		status = false;
	}
	
	public string name;
	protected int capacity;
	public bool status;
	
	public string GetDetails()
	{
		return $"Attraction: {name}, Capacity: {capacity}";
	}
	
	public virtual void Start()
	{
		Console.WriteLine("The attraction is starting.");
	}
	
	public void OpenAttraction() { status = true; }
	public void CloseAttraction() { status = false; }	
	
	public abstract bool IsEligible(int age, int height);
}

public class ThrillRide : Attraction
{
	public ThrillRide(string name, int capacity, int minHeight) : base(name, capacity)
	{
		this.minHeight = minHeight;
	}
	
	private int minHeight;
	
	public override void Start()
	{
		Console.WriteLine($"Thrill Ride {name} is now starting. Hold on tight!");
	}
	
	public override bool IsEligible(int age, int height) => height >= minHeight;
}

public class FamilyRide : Attraction
{
	public FamilyRide(string name, int capacity, int minAge) : base(name, capacity)
	{
		this.minAge = minAge;
	}
	
	private int minAge;
	
	public override void Start()
	{
		Console.WriteLine($"Family Ride {name} is now starting. Enjoy the fun!");
	}
	
	public override bool IsEligible(int age, int height) => age >= minAge;
}

public class Staff
{
	public Staff(string name, string role)
	{
		this.name = name;
		this.role = role;
	}
	
	public string name;
	public string role;
	
	public void Work()
	{
		Console.WriteLine($"Staff {name} is performing their role: {role}");
	}
}


public class Manager : Staff
{
	public Manager(string name, string role) : base(name, role)
	{
		this.team = new List<Staff>();
	}
	
	private List<Staff> team;
	
	public void AddStaff(Staff staff)
	{
		team.Add(staff);
	}
	
	public void GetTeamSummary()
	{
		Console.WriteLine(string.Join(", ", team.Select(x => $"{x.name} : {x.role}")));
	}
	
	
}

public class Visitor
{
	public Visitor(string name, int age, int height)
	{
		this.name = name;
		this.age = age;
		this.height = height;
		this.rideHistory = new List<string>();
	}
	
	private string name;
	private int age;
	private int height;
	private List<string> rideHistory;
	
	public void RideAttraction(Attraction attraction)
	{
		if (attraction.IsEligible(age, height))
		{
			Console.WriteLine($"{name} is eligible for the {attraction.name}");
			if (attraction.status)
			{
				rideHistory.Add(attraction.name);
				attraction.Start();
			}
			else
			{
				Console.WriteLine($"{attraction.name} is not open.");	
			}	
		}
		else
		{
			Console.WriteLine($"{name} is not eligible for the {attraction.name}");	
		}
	}
	
	public string ViewHistory()
	{
		return string.Join("\n", rideHistory);	
	}
}

public class Program
{
    public static void Main(string[] args)
    {
        ThrillRide thrillRide = new ThrillRide("Dragon Coaster", 20, 140);
		FamilyRide familyRide = new FamilyRide("Merry-Go-Round", 30, 4);
		
		Visitor visitor = new Visitor("Azmat", 20, 140);
		
		Staff staff1 = new Staff("Jake", "Cleaner");
		Staff staff2 = new Staff("Ab", "RideStarter");
		Manager manager = new Manager("Bob", "RideManager");
		
		manager.AddStaff(staff1);
		manager.AddStaff(staff2);
		
		manager.GetTeamSummary();
		
		
		visitor.RideAttraction(thrillRide);
		thrillRide.OpenAttraction();
		familyRide.OpenAttraction();
		visitor.RideAttraction(thrillRide);
		visitor.RideAttraction(familyRide);
		
		Console.WriteLine(visitor.ViewHistory());		
		
    }
}