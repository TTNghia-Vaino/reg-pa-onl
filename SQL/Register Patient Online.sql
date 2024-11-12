use RegisterPatientOnline
-- Tạo bảng Tài Khoản
CREATE TABLE TAI_KHOAN (
    MA_TK INT PRIMARY KEY IDENTITY(1,1), -- Khóa chính với IDENTITY
    TEN_DANG_NHAP VARCHAR(50) NOT NULL UNIQUE,   -- Tên đăng nhập
    MAT_KHAU VARCHAR(255) NOT NULL,       -- Mật khẩu
    ROLE INT NOT NULL                      -- Vai trò (0: Bệnh Nhân, 1: Quản Lý)
);



-- Tạo bảng Quản Lý
CREATE TABLE QUAN_LY (
    MA_QL INT PRIMARY KEY IDENTITY(1,1),   -- Khóa chính với IDENTITY
    MA_TK INT,                              -- Khóa ngoại
    TEN_QL VARCHAR(100) NOT NULL,          -- Tên của quản lý
    SDT VARCHAR(15),                       -- Số điện thoại
    EMAIL VARCHAR(100),                    -- Địa chỉ email
    FOREIGN KEY (MA_TK) REFERENCES TAI_KHOAN(MA_TK) ON DELETE CASCADE -- Ràng buộc khóa ngoại
);

-- Tạo bảng Bảo Hiểm Y Tế
CREATE TABLE BAO_HIEM_Y_TE (
    MA_BHYT VARCHAR(15) PRIMARY KEY,      -- Khóa chính
    TEN VARCHAR(100) NOT NULL,            -- Tên bảo hiểm
    GIOI_TINH INT NOT NULL,               -- Giới tính (0: Nữ, 1: Nam)
    NGAY_BD DATE NOT NULL,                 -- Ngày bắt đầu
    NGAY_HH DATE NOT NULL,                 -- Ngày hết hạn
    NGAY_SINH DATE NOT NULL                -- Ngày sinh của người dùng
);
-- Tạo bảng Bệnh Nhân
CREATE TABLE BENH_NHAN (
    MA_BN VARCHAR(100) PRIMARY KEY,       -- Khóa chính
    MA_TK INT UNIQUE,                             -- Khóa ngoại
    HOTEN VARCHAR(255) NOT NULL,          -- Họ và tên
    SDT VARCHAR(15),                       -- Số điện thoại
    EMAIL VARCHAR(100),                    -- Địa chỉ email
    DIA_CHI VARCHAR(255),                  -- Địa chỉ cư trú
    CCCD VARCHAR(15),                      -- Số căn cước công dân
    MA_BHYT VARCHAR(15) UNIQUE,                   -- Mã bảo hiểm y tế
    NGAY_SINH VARCHAR(12),                 -- Ngày sinh
    FOREIGN KEY (MA_TK) REFERENCES TAI_KHOAN(MA_TK) ON DELETE CASCADE, -- Ràng buộc khóa ngoại
	FOREIGN KEY (MA_BHYT) REFERENCES BAO_HIEM_Y_TE(MA_BHYT) ON DELETE SET NULL -- Ràng buộc khóa ngoại đến BAO_HIEM_Y_TE
);
-- Tạo bảng Tin Tức
CREATE TABLE TIN_TUC (
    MA_TIN_TUC INT PRIMARY KEY IDENTITY(1,1), -- Khóa chính với IDENTITY
    NOI_DUNG TEXT NOT NULL,                   -- Nội dung tin tức
    NGAY_DANG DATE DEFAULT GETDATE()          -- Ngày đăng tin (sử dụng GETDATE cho ngày hiện tại)
);

-- Tạo bảng Đăng Ký Khám
CREATE TABLE DANG_KY_KHAM (
    MA_DK INT PRIMARY KEY IDENTITY(1,1),     -- Khóa chính với IDENTITY
    MA_BN VARCHAR(100),                       -- Khóa ngoại đến Bệnh Nhân
    NGAY_DANG_KI DATE NOT NULL,               -- Ngày đăng ký
    GIO_DANG_KI TIME NOT NULL,                -- Giờ đăng ký
    KHOA VARCHAR(100) NOT NULL,               -- Khoa khám
    PHONG_KHAM VARCHAR(100) NOT NULL,         -- Phòng khám
    FOREIGN KEY (MA_BN) REFERENCES BENH_NHAN(MA_BN) ON DELETE CASCADE -- Ràng buộc khóa ngoại
);
