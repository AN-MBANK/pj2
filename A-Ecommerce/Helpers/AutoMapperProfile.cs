using AutoMapper;
using A_Ecommerce.Data;
using A_Ecommerce.ViewModels;

namespace A_Ecommerce.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // ánh xạ từ RegisterVM sang KhachHang
            CreateMap<RegisterVM, KhachHang>();
            // Nếu muốn ánh xạ ngược thì thêm:
            // CreateMap<KhachHang, RegisterVM>();
        }

        private void CreateMap<T1, T2>()
        {
            throw new NotImplementedException();
        }
    }
}
