namespace PhucPhuongCare.CoreBusiness.Enums
{
    public enum AppointmentStatus
    {
        PendingConfirmation, // Chờ xác nhận
        Confirmed,           // Đã xác nhận
        Completed,           // Đã hoàn thành
        CanceledByPatient,   // Bệnh nhân hủy
        CanceledByClinic,    // Phòng khám hủy
        NoShow               // Bệnh nhân không đến
    }
}