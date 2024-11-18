using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Threading;

@model IEnumerable<Register_Patient_Online.Models.DangKyKhamBenh>

<h2>Danh sách đăng ký khám bệnh</h2>
<table class= "table" >
    < thead >
        < tr >
            < th > ID </ th >
            < th > Họ Tên </ th >
            < th > Ngày Khám </ th >
            < th > Số Điện Thoại</th>
            <th>Ghi Chú</th>
        </tr>
    </thead>
    <tbody>
        @foreach (var item in Model)
{
            < tr >
                < td > @item.Id </ td >
                < td > @item.HoTen </ td >
                < td > @item.NgayKham.ToString("dd/MM/yyyy") </ td >
                < td > @item.SoDienThoai </ td >
                < td > @item.GhiChu </ td >
            </ tr >
        }
    </ tbody >
</ table >

< a class= "btn btn-primary" href = "/DangKyKhamBenh/Create" > Thêm mới </ a >

