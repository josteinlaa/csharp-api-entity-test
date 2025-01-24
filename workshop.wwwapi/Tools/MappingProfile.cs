using System;
using AutoMapper;

namespace workshop.wwwapi.Tools;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Models.Patient, DTO.PatientDTO>();
        CreateMap<Models.Appointment, DTO.AppointmentDTO>();
        CreateMap<Models.Doctor, DTO.DoctorDTO>();
    }
}
