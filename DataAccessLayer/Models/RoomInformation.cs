﻿using System.Text.Json.Serialization;

namespace DataAccessLayer.Models;

public partial class RoomInformation
{
    public int RoomId { get; set; }

    public string RoomNumber { get; set; } = null!;

    public string? RoomDetailDescription { get; set; }

    public int? RoomMaxCapacity { get; set; }

    public int RoomTypeId { get; set; }

    public byte? RoomStatus { get; set; }

    public decimal? RoomPricePerDay { get; set; }
    [JsonIgnore]
    public virtual ICollection<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();

    public virtual RoomType RoomType { get; set; } = null!;
}
