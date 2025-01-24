using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using workshop.wwwapi.DTO;
using workshop.wwwapi.Models;
using workshop.wwwapi.Repository;

namespace workshop.wwwapi.Endpoints
{
    public static class SurgeryEndpoint
    {
        //TODO:  add additional endpoints in here according to the requirements in the README.md 
        public static void ConfigurePatientEndpoint(this WebApplication app)
        {
            var surgeryGroup = app.MapGroup("surgery");

            surgeryGroup.MapGet("/patients", GetPatients);
            surgeryGroup.MapGet("patients/{id}", GetPatientById);
            surgeryGroup.MapPost("/patients", CreatePatient);
            surgeryGroup.MapPut("patients/{id}", UpdatePatient);                

            surgeryGroup.MapGet("/doctors", GetDoctors);


            surgeryGroup.MapGet("/appointmentsbydoctor/{id}", GetAppointmentsByDoctor);
        }

        private static string baseUrl(HttpContext context) {
            return $"{context.Request.Scheme}://{context.Request.Host}";
        }

        private static async Task UpdatePatient(HttpContext context)
        {
            throw new NotImplementedException();
        }

        private static async Task<IResult> CreatePatient(IRepository<Patient> repository, IMapper mapper, PatientPost patientPost, HttpContext context)
        {
            try
            {
                Patient patient = mapper.Map<Patient>(patientPost);
                patient = await repository.Create(patient);

                
                var patientDto = mapper.Map<PatientDTO>(patient);
                return Results.Created($"{baseUrl(context)}/surgery/patients/{patient.Id}", patientDto);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        private static async Task<IResult> GetPatientById(IRepository<Patient> repository, IMapper mapper, int id, HttpContext context)
        {
            try
            {
                var patient = await repository.GetById(id);
                if (patient == null)
                {
                    return TypedResults.NotFound();
                }

                var patientDto = mapper.Map<PatientDTO>(patient);
                return TypedResults.Ok(patientDto);
            }
            catch (Exception ex)
            {     
                return Results.Problem(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetPatients(IRepository<Patient> repository, IMapper mapper)
        {
            var results = await repository.GetWithIncludes(p => p.Appointments);
            var response = mapper.Map<IEnumerable<PatientDTO>>(results);

            return TypedResults.Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetDoctors(IRepository<Doctor> repository, IMapper mapper)
        {
            var results = await repository.GetAll();
            var response = mapper.Map<IEnumerable<DoctorDTO>>(results);

            return TypedResults.Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetAppointmentsByDoctor(IRepository<Doctor> repository, int id, IMapper mapper)
        {
            var results = await repository.GetWithIncludes(p => p.Appointments);
            var response = mapper.Map<IEnumerable<DoctorDTO>>(results);

            return TypedResults.Ok(response);
        }
    }
}
