const Resource = {
    VN: {
        completeExercise: {
            success: 'Bài tập đã được hoàn thành'
        },
        deleteExercise: {
            deleteConfirm: 'Bạn có chắc chắn muốn xóa bài tập này không?'
        },
        deleteQuestion: {
            deleteConfirm: `Bạn có chắc chắn muốn xóa giáo viên đang chọn không?`,
            multiDeleteConfirm: `Bạn có chắc chắn muốn xóa những giáo viên đang chọn không?`,
            deleteSuccess: 'Xóa câu hỏi thành công'
        },
        addQuestion: {
            addSuccess: 'Lưu câu hỏi thành công',
            closeConfirm: "Dữ liệu đã bị thay đổi, bạn có muốn lưu lại không?"
        },
        editQuestion: {
            editSuccess: 'Sửa câu hỏi thành công'
        },
        validate: {
            formatFile: "Định dạng file không hợp lệ",
            exerciseName: 'Tên bài tập không được bỏ trống',
            subject: 'Môn học không được bỏ trống',
            grade: 'Khối lớp không được bỏ trống',
        },
        response: {
            success: 'Thực hiện hành động thành công',
            warning: 'Có sự cố xảy ra, vui lòng kiểm tra lại',
            error: 'Có lỗi xảy ra, vui lòng kiểm tra lại'
        },
        toastType: {
            success: 'Thành công',
            warning: 'Cảnh báo',
            error: 'Lỗi'
        },
        save: 'Lưu',
        close: 'Đóng',
        delete: 'Xóa',
        pageSize: 'dòng/trang',
        excelFileName: 'Danh_sach_can_bo_giao_vien.xlsx',
    },

    EN: {
        deleteEmployee: {
            deleteConfirm: `Are you sure you want to delete selected teacher?`,
            multiDeleteConfirm: `Are you sure you want to delete selected teachers?`,
            deleteSuccess: 'Delete teacher successfully'
        },
        addEmployee: {
            addSuccess: 'Add teacher successfully',
            closeConfirm: "The data has been changed, do you want to save it?"
        },
        editEmployee: {
            editSuccess: 'Edit teacher successfully'
        },
        validate: {
            fullNameEmpty: 'Full name can not be empty',
            teacherCodeEmpty: 'Teacher code can not be empty',
            teacherCodeInvalid: 'Teacher code must end with a digit',
            emailInvalid: 'Email format is incorrect',
            leavingDateInvalid: 'Leaving date format is incorrect',
            leavingDateSoMuch: "The leave date cannot be greater than the current date",
        },
        response: {
            success: 'Take action successfully',
            warning: 'Something went wrong, please check again',
            error: 'An error occurred, please check again'
        },
        toastType: {
            success: 'Success',
            warning: 'Warning',
            error: 'Error'
        },
        save: 'Save',
        close: 'Close',
        delete: 'Delete',
        pageSize: 'line/page',
        excelFileName: 'Danh_sach_can_bo_giao_vien.xlsx',
    }
}

export default Resource;