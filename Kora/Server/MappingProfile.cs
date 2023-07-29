using AutoMapper;
using Kora.Models;
using Kora.Server.ModelsDto;

namespace Kora.Server;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Agence, AgenceDto>().ReverseMap();
        CreateMap<Administrateur, AdministrateurDto>().ReverseMap();
        CreateMap<Models.Client, ClientDto>().ReverseMap();
        CreateMap<Compte, CompteDto>().ReverseMap();
        CreateMap<Kiosque, KiosqueDto>().ReverseMap();
        CreateMap<Pays, PaysDto>().ReverseMap();
        CreateMap<ResponsableAgence, ResponsableAgenceDto>().ReverseMap();
        CreateMap<Ville, VilleDto>().ReverseMap();

    }
}