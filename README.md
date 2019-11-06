# BatchRename
Batch Rename v.1.0.0
1712426 - Võ Minh Hiền
1712393 - Trịnh Hoàng Khánh Duy
1712444 - Vũ Hoàng Hiếu

Phần mềm có đầy đủ các chức năng như yêu cầu. (Đánh giá đồ án: 100%)

Instructions: 
B1: Mở chương trình
B2: Chọn Rename Files hoặc Rename Folder để thực hiện thay đỏi.
B3: Chọn các chức năng từ cột bên trái, sau đó tùy chỉnh nội dung cần thực hiện
B4: Bấm Add to List để thêm các hành động vào danh sách chờ thực hiện
*Sau khi thêm thì bên tab AddList sẽ hiển thị các hành động vừa thêm.
*Các nút check cho phép tùy chọn những hành động sẽ thực hiện
*Nếu muốn xóa thì bấm chuột phải vào rồi chọn.
B5: Nếu muốn xem trước thì bấm nút Preview. Nếu muốn thực hiện ngay thì bấm Start Batch
*Trường hợp đang tùy chỉnh mà muốn quay về trạng thái ban đầu thì bấm nút Refresh

Nếu muốn lưu lại các hành động vừa thực hiện thì bấm vào nút Save ở tab AddList. Preset được lưu dưới dạng tập tin .txt
Nếu muốn thực hiện Preset đã lưu, bấm nút Open ở tab AddList để mở. Rồi bấm Start Batch để thực hiện


Các điểm nổi bật:
-Tạo được màn hình dialog cấu hình riêng cho từng chức năng nằm trên cùng một màn hình của chương trình chính
-Có nút Preview để người dùng xem kết quả và báo lỗi trước khi thực hiện thay đổi. Đặc biệt, các nút check giúp người dùng xem nhanh các thay đổi.
-Hỗ trợ click chuột phải để xóa hành động đã thêm vào danh sách.
-Các trường hợp tên file quá dài , chứa ký tự đặc biệt đều được xử lý kỹ càng.
*Độ dài giới hạn của Windows File: 259 (cả địnnh dạng) và Folder: 247 (cả địnnh dạng) . Không chứa :   /:*?" < > | .
-Khi đổi tên, nếu tên file và định dạng rỗng thì sẽ giữ nguyên, không đổi.
-Phần đổi vị trí chuỗi cho việc quản lý được nâng cấp lên trường hợp tổng quát. Có thể thực hiện ở vị trí bất kỳ, với số ký tự bất kỳ. (không bị giới hạn ở 13)
-Xử lý khi danh sách hành động trống sẽ không tạo Preset
-Giao diện đẹp, thân thiện với người dùng.
-Phần tab AddList hỗ trợ cac nút di chuyển để thay đổi thứ tự ưu tiên của các hành động.
-Đặc biệt, có thêm danh sách tùy chọn ở AddList khi tập tin bị trùng tên. Người dùng có thể chọn giữ nguyên tên cũ (Keep the old name) hoặc thêm hậu tố để tránh bị trùng
-Hỗ trợ xem chi tiết lỗi khi bấm chuột phải vào ở mỗi file. (errorDetail  sẽ hiện ra các action không thực hiện được hoặc các trường hợp như trùng tên file, tên file quá dài)
-Hỗ trợ nhiều tùy chọn xóa cho người dùng khi muốn bỏ hành động ra khỏi danh sách. (Xóa hành động hiện tại, xóa những mục đã check, xóa những mục chưa check)
-Hỗ trợ nhiều tùy chọn xóa ở danh sách các tập tin/ thư mục. (xóa tên file hiện tại khỏi danh sách, xóa tất cả mục cùng định dạng, xóa tất cả file ở cùng 1 thư mục)
-Hỏi lại người dùng trước khi thực hiện hành động xóa và thay đổi.
