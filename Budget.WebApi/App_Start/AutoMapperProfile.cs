using AutoMapper;
using Budget.DAL;
using Budget.Model;
using Budget.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Budget.App_Start
{
    public class AutoMapperProfile :Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ExpenseDTO, ExpenseRest>()
                .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.Person.Id))
                .ForMember(dest => dest.Person, opt => opt.MapFrom(src=> src.Person))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src=> src.Category.Id))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src=> src.Category));

            CreateMap<ExpenseRest, ExpenseDTO>()
                .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.Person.Id))
                .ForMember(dest => dest.Person, opt => opt.MapFrom(src => src.Person))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category.Id))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));

            CreateMap<Expense, ExpenseDTO>()
                .ForMember(dest => dest.Person, opt => opt.MapFrom(src => src.Person))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));

            CreateMap<ExpenseDTO, Expense>()
                .ForMember(dest => dest.Person, opt => opt.MapFrom(src => src.Person))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));

            CreateMap<PersonRest, PersonDTO>();
            CreateMap<PersonDTO, PersonRest>();

            CreateMap<Person, PersonDTO>();
            CreateMap<PersonDTO, Person>();

            CreateMap<CategoryDTO, CategoryRest>();
            CreateMap<CategoryRest, CategoryDTO>();

            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryDTO, Category>();
        }
    }
}