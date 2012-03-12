using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public class User
{
    public int Id { get; set; }

    [StringLength(128)]
    public string Email { get; set; }

    [StringLength(30)]
    public string Password { get; set; }

    public string Name { get; set; }
}