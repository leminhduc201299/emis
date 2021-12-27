<template lang="">
  <div>
    <!-- Content -->
        <div class="search-bar">
            <div class="search-left">
                <input v-on:keyup.enter="onEnterSearchText" class="m-input m-input-icon icon-search" style="width: 300px;" type="text" placeholder="Nhập số hiệu cán bộ hoặc Tên cán bộ để tìm kiếm">
            </div>
    
            <div class="search-right">
                <button @click="showTeacherDetail(myEnum.formMode.add, null)" class="m-btn m-btn-default">
                    Thêm
                </button>
                <button @click="exportTeacherList" class="m-btn m-second-btn">
                    Xuất khẩu
                </button>
                <button @click="showMultiDelete" class="m-btn m-btn-action icon-more">
                </button>
                <div v-show="isShowMultiDelete" @click="btnDeleteMultiOnClick" class="m-more m-cursor-pointer">
                  <div class="m-more-icon icon-delete"></div>
                  <div class="m-more-text">Xóa</div>
                </div>
            </div>
        </div> 

        <div class="gird">
            <table  class="m-table" cellspacing="0">
                <thead>
                    <tr>
                        <th class="align-right">
                            <div 
                              @click="checkAllTeacher()"
                              class="m-cursor-pointer m-table-checkbox icon-checkbox-inactive"
                              :class="{'icon-checkbox-active': teacherIdsForDelete && teacherIdsForDelete.length && teacherIdsForDelete.length == teachers.length}"
                            ></div>
                        </th>
                        <th class="align-center">
                            Số hiệu cán bộ
                        </th>
                        <th class="align-center">
                            Họ và tên
                        </th>
                        <th class="align-center">
                            Số điện thoại
                        </th>
                        <th class="align-center">
                            Tổ chuyên môn
                        </th>
                        <th class="align-center">
                            QL theo môn
                        </th>
                        <th class="align-center">
                            QL kho, phòng
                        </th> 
                        <th class="align-center">
                            Đào tạo QLTB
                        </th>
                        <th class="align-center">
                            Đang làm việc
                        </th>
                        <th style="border-right: none;" class="align-center">
                            
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr 
                        v-for="teacher in teachers" :key="teacher.TeacherId"
                        @dblclick="showTeacherDetail(myEnum.formMode.edit, teacher)"
                        @click="rowOnClick(teacher.TeacherId)"
                    >
                        <td style="max-width: 38.5px;" class="align-right">
                            <div 
                              @click.stop="selectTeacherIdForDelete(teacher.TeacherId)"
                              :class="{'icon-checkbox-active': teacherIdsForDelete.includes(teacher.TeacherId)}"
                              class="m-cursor-pointer m-table-checkbox icon-checkbox-inactive"
                            ></div>
                        </td>
                        <td class="align-left">
                            <div :title="teacher.TeacherCode" style="min-width: 100px; width: 109px;" class="text-cell">
                              {{ teacher.TeacherCode }}
                            </div>
                        </td>
                        <td class="align-left">
                          <div 
                            @click="showTeacherDetail(myEnum.formMode.edit, teacher)" 
                            :title="teacher.FullName" 
                            style="min-width: 140px; width: 149px; color: #01C573;" 
                            class="text-cell m-cursor-pointer"
                          >
                            {{ teacher.FullName }}
                          </div>
                        </td>
                        <td class="align-left">
                          <div :title="teacher.PhoneNumber" style="min-width: 90px; width: 99px;" class="text-cell">
                            {{ teacher.PhoneNumber }}
                          </div>
                        </td>
                        <td class="align-left">
                          <div :title="teacher.DepartmentName" style="min-width: 120px; width: 129px;" class="text-cell">
                            {{ teacher.DepartmentName }}
                          </div>
                        </td>
                        <td class="align-left">
                          <div :title="teacher.SubjectNames | formatString" style="min-width: 120px; width: 129px;" class="text-cell">
                            {{ teacher.SubjectNames | formatString }}
                          </div>
                        </td>
                        <td class="align-left">
                          <div :title="teacher.EquipmentRoomNames | formatString" style="min-width: 170px; width: 179px;" class="text-cell">
                            {{ teacher.EquipmentRoomNames | formatString }}
                          </div>
                        </td> 
                        <td class="align-center">
                          <div style="min-width: 100px;" class="m-table-action-icon" :class="{ 'icon-check': teacher.EquipmentQualification }"></div>
                        </td>
                        <td class="align-center">
                          <div style="min-width: 100px;" class="m-table-action-icon" :class="{ 'icon-check': teacher.WorkStatus }"></div>
                        </td>
                        <td style="min-width: 40px; " class="align-center m-table-action">
                            <div @click="showTeacherDetail(myEnum.formMode.edit, teacher)" class="m-table-action-icon icon-edit"></div>
                            <div @click="btnDeleteOnClick(teacher.TeacherId)" class="m-table-action-icon icon-remove"></div>
                        </td>
                    </tr>
                </tbody>
            </table>
            <div class="m-nodata" v-if="!(teachers && teachers.length)">Không có dữ liệu</div>
        </div>

      <div class="navigation">
        <!-- Phần phân trang -->
        <BasePaging 
          v-model="keySearch.pageNumber"
          :totalPage="totalPage"
          :totalRecord="totalRecord"
        /> 

        <div class="navigation-right">
            <!-- <BaseComboBox
                :options="pageSizeOptions"
                 v-model="keySearch.pageSize"
                placeholder=''
                :tabindex="0"
                :isSelectTop="true"
            /> -->
        </div>
      </div>

    <!-- Dialog Employee Info -->
    <TeacherDetail 
        v-show="isShowTeacherDetail" 
        :formMode="formMode"
        @closeTeacherDetail="closeTeacherDetail"
        @editTeacher="editTeacher"
        @addTeacher="addTeacher"
        :teacherForEdit="teacherForEdit"
    />
    
  </div>
</template>
<script>
import myEnum from "../../utils/enum.js";
import request from "../../utils/request";
import TeacherDetail from "./TeacherDetail.vue";
import BasePaging from "../../components/base/BasePaging";
// import BaseComboBox from "../../components/base/BaseComboBox";
import Toastify from "../../utils/toastify";
import Resource from "../../utils/resource";
import CommonJS from "../../utils/common";

export default {
  name: "teacher-list",

  components: {
    TeacherDetail,
    BasePaging,
    // BaseComboBox,
  },

  data() {
    return {
      teachers: [],
      isShowTeacherDetail: false,
      formMode: myEnum.formMode.view,
      isShowMultiDelete: false,

      teacherForEdit: null,
      teacherIdsForDelete: [],
      myEnum: myEnum,

      keySearch: {
        pageSize: 20,
        pageNumber: 1,
        searchText: "",
      },
      totalPage: 0,
      totalRecord: 0,

      pageSizeOptions: [
        { text: `10 ${Resource[CommonJS.languageCode].pageSize}`, value: 10 },
        { text: `20 ${Resource[CommonJS.languageCode].pageSize}`, value: 20 },
        { text: `30 ${Resource[CommonJS.languageCode].pageSize}`, value: 30 },
        { text: `50 ${Resource[CommonJS.languageCode].pageSize}`, value: 50 },
        { text: `70 ${Resource[CommonJS.languageCode].pageSize}`, value: 70 },
        { text: `100 ${Resource[CommonJS.languageCode].pageSize}`, value: 100 },
      ],
    };
  },

  methods: {
    /**
     * Method để load nhân viên trong CSDL
     * Author: LMDuc (18/11/2021)
     */
    loadData() {
      let me = this;

      let keysearch = me.keySearch;
      request(
        {
          url: `/Teachers/Filter?searchText=${keysearch.searchText}&pageSize=${keysearch.pageSize}&pageNumber=${keysearch.pageNumber}`,
          method: "GET",
        }, false
      )
        .then((res) => {
          me.teachers = res.Data.Data;
          me.totalPage = res.Data.TotalPage;
          me.totalRecord = res.Data.TotalRecord;
        })
        .catch((error) => {
          console.log(error);
        });
    },

    /**
     * Sự kiện khi nhấn enter vào ô text search
     * Author: LMDuc (22/11/2021)
     */
    onEnterSearchText(event) {
      let value = event.target.value;

      // Chuyển text search sang dạng viết thường và xóa khoảng trắng ở đầu và cuối
      value = value.toLowerCase().trim();

      this.keySearch.pageNumber = 1;
      this.keySearch.searchText = value;
    },

    /**
     * Method mở dialog nhân viên theo form mode
     * Author: LMDuc (19/11/2021)
     */
    showTeacherDetail(formMode, teacher) {
      this.formMode = formMode;
      this.teacherForEdit = teacher;

      this.isShowTeacherDetail = true;
    },

    /**
     * Method để đóng dialog thông tin nhân viên
     * Author: LMDuc (19/11/2021)
     */
    closeTeacherDetail() {
      this.formMode = myEnum.formMode.view;
      this.isShowTeacherDetail = false;
    },

    /**
     * Method hiển thị nút xóa nhiều nhân viên
     * Author: LMDuc (22/11/2021)
     */
    showMultiDelete() {
      this.isShowMultiDelete = !this.isShowMultiDelete;
    },

    /**
     * Sự kiện khi click vào row trong table
     * Author: LMDuc (19/11/2021)
     */
    rowOnClick(teacherId) {
      this.teacherIdsForDelete = [teacherId];
    },

    /**
     * Sự kiện khi click vào checkbox trong table
     * Author: LMDuc (20/11/2021)
     */
    selectTeacherIdForDelete(teacherId) {
      if (this.teacherIdsForDelete.includes(teacherId)) {
        this.teacherIdsForDelete = this.teacherIdsForDelete.filter(
          (itemSelectd) => itemSelectd !== teacherId
        );
      } else {
        this.teacherIdsForDelete.push(teacherId);
      }
    },

    /**
     * Sự kiện khi click vào checkbox all trong table
     * Author: LMDuc (21/11/2021)
     */
    checkAllTeacher() {
      if (this.teacherIdsForDelete && this.teacherIdsForDelete.length &&
        this.teacherIdsForDelete.length == this.teachers.length
      ) {
        this.teacherIdsForDelete = [];
      } else {
        this.teacherIdsForDelete = this.teachers.map(
          (teacher) => teacher.TeacherId
        );
      }
    },

    /**
     * Thêm giáo viên vào CSDL
     * Author: LMDuc (22/11/2021)
     */
    addTeacher(teacher) {
      let me = this;

      request(
        {
          method: "post",
          url: "/Teachers",
          data: teacher,
        }, true, true
      )
        .then((res) => {
          if (!res.Success) return;

          // Load lại dữ liệu
          // Kiểm tra xem người dùng có ở trang đầu không
          let keySearch = me.keySearch;
          if (keySearch.searchText != "" || keySearch.pageNumber !== 1) {
            me.keySearch.searchText = "";
            me.keySearch.pageNumber = 1;
          } else {
            me.loadData();
          }

          // Đóng dialog
          me.closeTeacherDetail();
        })
        .catch((error) => {
          console.log(error);
        });
    },

    /**
     * Sửa giáo viên trong CSDL
     * Author: LMDuc (22/11/2021)
     */
    editTeacher(teacherId, teacher) {
      let me = this;

      request(
        {
          method: "put",
          url: `/Teachers/${teacherId}`,
          data: teacher,
        }, true, true
      )
        .then((res) => {
          if (!res.Success) return;

          // Load lại dữ liệu
          this.loadData();

          // Đóng dialog
          me.closeTeacherDetail();
        })
        .catch((error) => {
          console.log(error);
        });
    },

    /**
     * Xóa nhân viên trong CSDL
     * Author: LMDuc (22/11/2021)
     */
    btnDeleteOnClick(teacherId) {
      if (teacherId) {
        this.teacherIdsForDelete = [teacherId];

        // Hiển thị cảnh báo cho ngươi dùng biết
        Toastify.showPopupMsg({
          yesCallback: () => this.deleteTeacherById(teacherId),
          title: "",
          iShowBtnNo: true,
          msg: Resource[CommonJS.languageCode].deleteEmployee.deleteConfirm,
        });
      } else {
        console.log();
      }
    },

    /**
     * Thực hiện Xóa nhân viên
     * Author: LMDuc (09/11/2021)
     */
    deleteTeacherById(teacherId) {
      let me = this;

      request(
        {
          method: "delete",
          url: "/Teachers",
          data: [teacherId],
        }, true
      )
        .then((res) => {
          console.log(res);

          // Load lại dữ liệu
          this.loadData();

          // Set lại giá trị null cho employeeIdForDelete
          me.teacherIdsForDelete = [];
        })
        .catch((error) => {
          console.log(error);
        });
    },

    /**
     * Xóa nhiều giáo viên trong CSDL
     * Author: LMDuc (22/11/2021)
     */
    btnDeleteMultiOnClick() {
      if (this.teacherIdsForDelete && this.teacherIdsForDelete.length) {
        this.isShowMultiDelete = !this.isShowMultiDelete;

        let popupMsg =
          this.teacherIdsForDelete.length > 1
            ? Resource[CommonJS.languageCode].deleteEmployee.multiDeleteConfirm
            : Resource[CommonJS.languageCode].deleteEmployee.deleteConfirm;
        // Hiển thị cảnh báo cho ngươi dùng biết
        Toastify.showPopupMsg({
          yesCallback: () => this.deleteMultiTeacher(this.teacherIdsForDelete),
          title: "",
          iShowBtnNo: true,
          msg: popupMsg,
        });
      } else {
        console.log();
      }
    },

    /**
     * Thực hiện Xóa nhân viên
     * Author: LMDuc (09/11/2021)
     */
    deleteMultiTeacher(teacherIdList) {
      let me = this;

      request(
        {
          method: "delete",
          url: `/Teachers`,
          data: teacherIdList,
        }, true
      )
        .then((res) => {
          console.log(res);

          // Load lại dữ liệu
          this.loadData();

          // Set lại giá trị null cho employeeIdForDelete
          me.teacherIdsForDelete = [];
        })
        .catch((error) => {
          console.log(error);
        });
    },

    /**
     * Sự kiện khi nhấn vào nút xuất khẩu danh sách giáo viên
     * Author: LMDuc (25/11/2021)
     */
    exportTeacherList() {
      request(
        {
          method: "get",
          url: `/Teachers/export`,
          responseType: "arraybuffer",
        }, false
      )
        .then((res) => {
          let blob = new Blob([res], {
            type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
          });
          let url = window.URL.createObjectURL(blob);

          var downloadLink = document.createElement("a");
          downloadLink.href = url;
          downloadLink.download = Resource[CommonJS.languageCode].excelFileName;

          document.body.appendChild(downloadLink);
          downloadLink.click();
          document.body.removeChild(downloadLink);
        })
        .catch((error) => {
          console.log(error);
        });
    },
  },

  created() {
    this.loadData();
  },

  filters: {
    // Xử lý dữ liệu string liên quan đến môn học, kho/phòng
    formatString: function (value) {
      if (value) {
        return value.replaceAll(",", ", ");
      }
    },
  },

  watch: {
    // Khi key search thay đổi thì load lại data
    keySearch: {
      handler() {
        this.loadData();
      },
      deep: true,
    },
  },
};
</script>
<style scope>
</style>