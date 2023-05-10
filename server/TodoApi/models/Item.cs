using System;
using System.Collections.Generic;

namespace TodoApi.models;

public partial class Item
{
    static int nextId = 1;
    public int Id { get; set; }

    public string? Name { get; set; }

    public bool? IsComplete { get; set; }
    public Item()
    {

    }
    public Item(string name)
    {
        this.Id = nextId++;
        this.Name = name;
        this.IsComplete = false;
    }
}
