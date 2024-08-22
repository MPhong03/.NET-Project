# ĐỒ ÁN CUỐI KỲ MÔN CÔNG NGHỆ .NET 

## Công nghệ sử dụng
ASP.NET REST API, Blazor, SQL, Firebase.

## Xây dựng website
### **Các tác nhân trong hệ thống**

| **STT** | **Tác nhân** | **Mô tả**                                                                                                                                                                                                                                                                                                                                                                                        |
|---------|--------------|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| 1       | Admin        | Được cấp sẵn tài khoản, đồng thòi Admin có thể phân quyền cho một User khác. Có quyền được truy cập vào trang Admin, thực hiện các tác vụ liên quan tới dữ liệu: Quản lý, phân quyền tài khoản người dùng. Quản lý danh sách diễn viên, đạo diễn. Quản lý danh sách thể loại, quốc gia. Quản lý danh sách phim bộ, phim lẻ. Quản lý danh sách các tập phim lẻ. Xem dữ liệu thống kê các bộ phim. |
| 2       | User         | Có thể đăng ký tài khoảng thông qua trang đăng ký. User có quyền được thay đổi mật khẩu thông qua xác thực OTP. User được phép truy cập vào website để xem phim, tìm kiếm phim, lọc phim, lưu phim vào bộ sưu tập của bản thân.                                                                                                                                                                  |

### **Các Use Case trong hệ thống**

| **ID** | **Tên Use Case**      | **Tác nhân** | **Mô tả**                                                                                                                                             |
|--------|-----------------------|--------------|-------------------------------------------------------------------------------------------------------------------------------------------------------|
| UC01   | Đăng nhập             | Tất cả       | Đăng nhập vào hệ thống bằng tài khoản đã đăng ký để truy cập vào website                                                                              |
| UC02   | Đăng ký               | Tất cả       | Người dùng có thể đăng ký tài khoản để sử dụng các chức năng bắt buộc phải đăng nhập                                                                  |
| UC03   | Đăng xuất             | Tất cả       | Các tác nhân đã đăng nhập vào website có thể đăng xuất ra khỏi website                                                                                |
| UC04   | Phân quyền người dùng | Admin        | Tác nhân quản lý tài khoản người dùng có thể phân vai trò cho tài khoản đó trở thành Admin hoặc User                                                  |
| UC05   | Xóa người dùng        | Admin        | Tác nhân được phép xóa đi một, hoặc nhiều tài khoản người dùng có trong cơ sớ dữ liệu                                                                 |
| UC06   | Thêm quốc gia         | Admin        | Tác nhân có thể thêm một quốc gia vào cơ sở dữ liệu để dễ dàng cho việc thêm là lọc phim có quốc gia tương ứng                                        |
| UC07   | Xóa quốc gia          | Admin        | Tác nhân có thể xóa một hoặc nhiều quốc gia ra khỏi cơ sở dữ liệu                                                                                     |
| UC08   | Sửa quốc gia          | Admin        | Tác nhân có thể đổi tên một quốc gia có sẵn tỏng cơ sở dữ liệu                                                                                        |
| UC09   | Thêm diễn viên        | Admin        | Tác nhân có thể thêm dữ liệu của một diễn viên                                                                                                        |
| UC10   | Xóa diễn viên         | Admin        | Tác nhân có thể xóa dữ liệu của một hoặc nhiều diễn viên                                                                                              |
| UC11   | Sửa diễn viên         | Admin        | Tác nhân có thể chỉnh sửa thông tin của một diễn viên                                                                                                 |
| UC12   | Thêm đạo diẽn         | Admin        | Tác nhân có thể thêm dữ liệu của một đạo diễn                                                                                                         |
| UC13   | Xóa đạo diễn          | Admin        | Tác nhân có thể xóa dữ liệu của một hoặc nhiều đạo diễn                                                                                               |
| UC14   | Sửa đạo diễn          | Admin        | Tác nhân có thể chỉnh sửa thông tin của một đạo diễn                                                                                                  |
| UC15   | Thêm phim lẻ          | Admin        | Tác nhân có thể thêm thông tin của một bộ phim lẻ                                                                                                     |
| UC16   | Xóa phim lẻ           | Admin        | Tác nhân có thể xóa một hoặc nhiều bộ phim lẻ ra khỏi cơ sở dữ liệu                                                                                   |
| UC17   | Sửa phim lẻ           | Admin        | Tác nhân có thể chỉnh sửa thông tin của một bộ phim lẻ                                                                                                |
| UC18   | Thêm phim bộ          | Admin        | Tác nhân có thể thêm thông tin của một bộ phim bộ, đồng thời tác nhân có thể thêm tập phim                                                            |
| UC19   | Xóa phim bộ           | Admin        | Tác nhân có thể xóa một hoặc nhiều bộ phim bộ ra khỏi cơ sở dữ liệu                                                                                   |
| UC20   | Sửa phim bộ           | Admin        | Tác nhân có thể chỉnh sửa thông tin của một bộ phim bộ                                                                                                |
| UC21   | Thêm tập phim         | Admin        | Trong quá trình thêm phim bộ hoặc chỉnh sửa một bộ phim bộ, tác nhân có thể thông qua hai tác vụ đó và thêm vào thông tin của tập phim cho phim bộ đó |
| UC22   | Xóa tập phim          | Admin        | Trong quá trình chỉnh sửa hoặc hiển thị danh sách các tập phim của một bộ phim bộ, tác nhân có thể xóa một hoặc nhiều tập phim của phim bộ đó         |
| UC23   | Sửa tập phim          | Admin        | Tác nhân có thể chỉnh sửa tập phim của phim bộ thông qua danh sách các tập phim của phim bộ đó                                                        |
| UC24   | Thêm thể loại         | Admin        | Tác nhân có thể thêm một thể loại vào cơ sở dữ liệu                                                                                                   |
| UC25   | Xóa thể loại          | Admin        | Tác nhân có thể xóa một hoặc nhiều thể loại                                                                                                           |
| UC26   | Sửa thể loại          | Admin        | Tác nhân có thể chỉnh sửa thông tin của một thể loại                                                                                                  |
| UC27   | Thống kê phim         | Admin        | Tác nhân có thể xem biểu đồ tròn của danh sách tối đa 50 phim có lượt xem nhiều nhất đến thời điển hiện tại                                           |
| UC28   | Tạo quảng cáo         | Admin        | Tác nhân có thể thêm một bộ phim vào banner quảng cáo được hiển thị ở trang chủ                                                                       |
| UC29   | Đổi mật khẩu          | Tất cả       | Tất cả các tác nhân có thể thay đổi mật khẩu thông qua xác thực email và xác thực mã OTP được hệ thống gửi đến email đang được xác thực               |
| UC30   | Tìm kiếm phim         | Tất cả       | Tất cả các tác nhân có thể tìm kiếm các bộ phim thông qua từ khóa, bộ lọc danh sách phim theo năm, quốc gia, thể loại, …                              |
| UC31   | Xem phim              | Tất cả       | Tất cả các tác nhân có thể xem phim được chọn và sẽ tăng lượt xem cho bộ phim đó                                                                      |
| UC32   | Lưu phim              | Tất cả       | Tất cả các tác nhân có thể thêm phim vào bộ sưu tập của bản thân                                                                                      |

## Phân chia công việc
| MSSV     | Tên SV          | Công việc                                                                                                                                                                                                                                                                                                               | Mức độ hoàn thành |
|----------|-----------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|-------------------|
| 52100033 | Lê Gia Huy      | Thiết kế cơ sở dữ liệu, vẽ ERD Giao diện Website Trang thông tin phim Trang danh sách phim bộ, phim lẻ Trang thông tin diễn viên  Trang thông tin đạo diễn Trang xem phim Kiểm thử Làm báo cáo                                                                                                                          | 100%              |
| 52100987 | Đặng Minh Phong | Đăng nhập, đăng ký, đổi mật khẩu bằng OTP Tạo Firebase Services Giao diện các trang Admin Tạo API Tìm kiếm phim Quản lý diễn viên, đạo diễn, thể loại, quốc gia Quản lý tài khoản người dùng Quản lý phim lẻ, phim bộ, tập phim Áp dụng Authentication và Authorization Lưu phim vào bộ sưu tập Trang xem phim Kiểm thử | 100%              |

[XEM DEMO]([https://youtu.be/ovlOeo5d2Bo](https://www.dropbox.com/scl/fi/f3246vdqp7zrz2qaqvjx2/DEMO-CU-I-K-C-NG-NGH-.NET.mp4?rlkey=hk6ja9jpjtmgpb9tjrk76fvvi&dl=0))
