namespace raag_api.Dtos;

public class MapMeasurementDto
{
    public int Id { get; set; }
    
    public int StationId { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public List<ValueDto> Values { get; set; }
}

public class ValueDto
{
    public DateTime Timestamp { get; set; }
    public string StationParameterName { get; set; }
    public double? Value { get; set; } 
}