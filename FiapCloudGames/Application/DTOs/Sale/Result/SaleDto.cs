﻿namespace Application.DTOs.Sale.Result;
public class SaleDto
{
    public int Id { get; set; }
    public DateTime CreationDate { get; set; }
    public bool IsActive { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal DiscountPercentage { get; set; }
}
