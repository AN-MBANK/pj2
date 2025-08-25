namespace A_Ecommerce.ViewModels
{
    public class CartItem
    {
        public int MaHh { get; set; }
        public string Hinh { get; set; } = string.Empty;
        public string TenHH { get; set; } = string.Empty;
        public double DonGia { get; set; }
        public int SoLuong { get; set; }
        public double ThanhTien => SoLuong * DonGia;
    }
}
