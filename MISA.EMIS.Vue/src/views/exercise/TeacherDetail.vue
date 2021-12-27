<template>
    <!-- Dialog -->
    <div class="m-dialog">
        <div class="m-dialog-modal"></div>

        <div class="m-dialog-box">
            <!-- Header -->
            <div class="m-dialog-header">
                <div tabindex="13" v-on:keyup.space="btnXOnClick" @click="btnXOnClick" class="m-dialog-header-icon m-dialog-header-icon-close icon-close m-cursor-pointer"></div>
                <div tabindex="14" @focus="onHelpFocus" class="m-dialog-header-icon m-dialog-header-icon-help icon-green-help m-cursor-pointer"></div>
            </div>

            <!-- Content -->
            <div class="m-dialog-content">
                <div class="m-dialog-content-left">
                    <div class="select-avatar">
                        <div class="employee-avatar"></div>
                        <div class="avatar-input">
                            <button class="m-btn m-btn-default">
                                Chọn ảnh
                            </button>
                        </div>
                    </div>
                    <div class="employee-info">
                        <p class="employee-info-name">{{ teacher.FullName ? teacher.FullName : 'Họ và tên' }}</p>
                        <p class="employee-info-code">{{ teacher.TeacherCode ? teacher.TeacherCode : 'TQT1' }}</p>
                    </div>
                </div>

                <div class="m-dialog-content-right">

                    <div class="m-dialog-content-info">
                        <div class="m-infor-title">Thêm hồ sơ Cán bộ, giáo viên</div>
                        <div class="form-info">
                            <div class="m-row">
                                <div class="m-form-group flex tooltip">
                                    <label class="m-input-label w-100" for="txtEmployeeCode">Họ và tên <span class="m-span-require">*</span></label>
                                    <input 
                                        ref="FullName" 
                                        v-model="teacher.FullName" 
                                        :class="{ 'm-input-invalid': validateError.FullName }"
                                        tabindex="1"
                                        class="m-input" style="width: 208px;" type="text"
                                    >
                                    <span v-if="validateError.FullName" class="tooltiptext tooltip-right">{{ validateError.FullName }}</span>
                                </div>
                                
                                <div class="m-form-group flex pl-25 tooltip">
                                    <label class="m-input-label w-110" for="txtFullName">Số hiệu cán bộ <span class="m-span-require">*</span></label>
                                    <input 
                                        ref="TeacherCode" 
                                        v-model="teacher.TeacherCode" 
                                        :class="{ 'm-input-invalid': validateError.TeacherCode }"
                                        tabindex="2"
                                        class="m-input" style="width: 208px;" type="text"
                                    >
                                    <span v-if="validateError.TeacherCode" class="tooltiptext tooltip-left">{{ validateError.TeacherCode }}</span>
                                </div>
                            </div>
        
                            <div class="m-row">
                                <div class="m-form-group flex" fieldName="DateOfBirth" formatType="ddmmyy">
                                    <label class="m-input-label w-100" for="txtDateOfBirth">Số điện thoại</label>
                                    <input  v-model="teacher.PhoneNumber" tabindex="3" class="m-input" style="width: 208px;" type="text">
                                </div>
        
                                <div class="m-form-group flex pl-25 tooltip">
                                    <label class="m-input-label w-110" for="name">Email</label>
                                    <input 
                                    ref="Email" 
                                    validateEmailInput v-model="teacher.Email" 
                                    :class="{ 'm-input-invalid': validateError.Email }"
                                    tabindex="4"
                                    class="m-input" style="width: 208px;" type="text">
                                    <span v-if="validateError.Email" class="tooltiptext tooltip-left">{{ validateError.Email }}</span>
                                </div>
                            </div>
        
                            <div class="m-row">
                                <div class="m-form-group flex" fieldName="IdentityNumber">
                                    <label class="m-input-label w-100" for="txtIdentityNumber">Tổ bộ môn</label>
                                    <BaseComboBox
                                        :options="departments"
                                        v-model="teacher.DepartmentId"
                                        placeholder=''
                                        :tabindex="5"
                                    />
                                </div>
        
                                <div class="m-form-group flex pl-25" fieldName="IdentityDate" formatType="ddmmyy">
                                    <label class="m-input-label w-110" for="txtIdentityDate">Môn dạy</label>
                                    <BaseMultiSelect
                                        :options="subjects"
                                        v-model="teacher.SubjectIds"
                                        placeholder=''
                                        :tabindex="6"
                                    />
                                </div>
                            </div>
        
                            <div class="m-row">
                                <div class="m-form-group  flex" fieldName="IdentityPlace">
                                    <label class="m-input-label w-100" for="txtIdentityPlace">QL kho, phòng</label>
                                    <BaseMultiSelect
                                        :options="equipmentRooms"
                                        v-model="teacher.EquipmentRoomIds"
                                        placeholder=''
                                        :fullWith="true"
                                        :showLength="4"
                                        :tabindex="7"
                                    />
                                </div>
                            </div>
        
                            <div class="m-row">
                                <div class="m-form-group  flex-4" fieldName="Email">
                                    <BaseCheckBox
                                        label="Trình độ nghiệp vụ QLTB"
                                        v-model="teacher.EquipmentQualification"
                                        :tabindex="8"
                                    />
                                </div>
        
                                <div class="m-form-group flex-3 pl-25" fieldName="PhoneNumber">
                                    <BaseCheckBox
                                        label="Đang làm việc"
                                        v-model="teacher.WorkStatus"
                                        :tabindex="9"
                                    />
                                </div>

                                <div :class="{'m-visibility-hidden': teacher.WorkStatus}" class="m-form-group flex-5 pl-25 tooltip" formatType="ddmmyy">
                                    <label class="m-input-label w-110" for="txtIdentityDate">Ngày nghỉ việc</label>
                                    <date-picker 
                                        v-model="teacher.LeavingDate" 
                                        format="DD/MM/YYYY"
                                        valueType="YYYY-MM-DD"
                                        :input-attr="{ 'tabindex': 10}"
                                        ref="LeavingDate"
                                        placeholder="dd/mm/yyyy"
                                        :input-class="{ 'mx-input': true, 'm-input-invalid': validateError.LeavingDate }"
                                    />
                                    <span v-if="validateError.LeavingDate" class="tooltiptext tooltip-left">{{ validateError.LeavingDate }}</span>
                                </div>
                            </div>
                        </div>
                        
                    </div>
                                        
                </div>
            </div>

            <!-- Footer -->
            <div class="m-dialog-footer">
                <div class="m-dialog-footer-button">
                    <button tabindex="12" @click="btnCloseOnClick" class="m-btn m-second-btn">
                        Đóng
                    </button>
                    <button tabindex="11" @click="btnSaveOnClick" class="m-btn m-btn-default">
                        Lưu
                    </button>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import myEnum from '../../utils/enum.js'
import Format from '../../utils/format'
import request from "../../utils/request";
import Validate from '../../utils/validate'
import BaseComboBox from '../../components/base/BaseComboBox.vue'
import BaseMultiSelect from '../../components/base/BaseMultiSelect.vue'
import BaseCheckBox from '../../components/base/BaseCheckBox.vue'
import Resource from '../../utils/resource';
import CommonJS from '../../utils/common';
import Toastify from "../../utils/toastify";
import DatePicker from 'vue2-datepicker';
import 'vue2-datepicker/index.css';

export default {
    name: 'teacher-detail',

    components: {
        BaseComboBox, 
        BaseMultiSelect,
        BaseCheckBox,
        DatePicker 
    },

    data() {
        return {
            teacher: {
                TeacherCode: "",
                FullName: "",
                PhoneNumber: "",
                Email: "",
                DepartmentId: "",
                SubjectIds: [],
                EquipmentQualification: false,
                WorkStatus: true,
                LeavingDate: "",
                EquipmentRoomIds: [],
            },
            
            validateError: {
                TeacherCode: "",
                FullName: "",
                Email: "",
                LeavingDate: "",
            },

            subjects: [],
            departments: [],
            equipmentRooms: []
        };
    },

    props: {
        formMode: {
            type: Number,
            default: myEnum.formMode.view,
        },

        teacherForEdit: {
            type: Object,
            default: null,
        }
    },

    methods: {
        /**
         * Sự kiện khi click vào button x
         * Author: LMDuc (16/11/2021)
         */
        btnXOnClick() {
            Toastify.showPopupMsg({
                yesCallback: () => this.btnSaveOnClick(), 
                title: "", 
                msg: Resource[CommonJS.languageCode].addEmployee.closeConfirm,
                iShowBtnClose: true,
                iShowBtnNo: true,
                btnSaveText: Resource[CommonJS.languageCode].save,
                noCallBack: () => this.$emit('closeTeacherDetail')
            })
        },

        /**
         * Sự kiện khi click vào button close
         * Author: LMDuc (22/11/2021)
         */
        btnCloseOnClick() {
            this.$emit('closeTeacherDetail')
        },

        /**
         * Sự kiện khi focus vào button help
         * Author: LMDuc (22/11/2021)
         */
        onHelpFocus() {
            this.$refs.FullName.focus();
        },
        
        /**
         * Sự kiện khi click vào button save
         * Author: LMDuc (16/11/2021)
         */
        btnSaveOnClick() {
            try {
                // Validate dữ liệu
                if (!this.validateInput()) {
                    return;
                }

                let teacher = this.teacher;
                if (this.formMode === myEnum.formMode.add) {
                    this.$emit('addTeacher', teacher);
                }
                else if (this.formMode === myEnum.formMode.edit) {
                    this.$emit('editTeacher', this.teacherForEdit.TeacherId, teacher);
                }
            } catch (error) {
                console.log(error)
            }
        },

        /**
         * Method để validate dữ liệu đầu vào
         * Author: LMDuc (20/11/2021)
         */
        validateInput() {
            let refElements = this.$refs;

            if (!this.validateFullName()) {
                refElements.FullName.focus();
                return false;
            }

            if (!this.validateTeacherCode()) {
                refElements.TeacherCode.focus();
                return false;
            }

            if (!this.validateEmail()) {
                refElements.Email.focus();
                return false;
            }

            if (!this.validateLeavingDate()) {
                refElements.LeavingDate.focus();
                return false;
            }

            return true;
        },

        /**
         * Method để validate FullName
         * Author: LMDuc (20/11/2021)
         */
        validateFullName() {
            if (!this.teacher.FullName) {
                this.validateError.FullName = Resource[CommonJS.languageCode].validate.fullNameEmpty;
                return false;
            }

            this.validateError.FullName = "";
            return true;
        },

        /**
         * Method để validate TeacherCode
         * Author: LMDuc (21/11/2021)
         */
        validateTeacherCode() {
            let teacherCode = this.teacher.TeacherCode;
            if (!teacherCode) {
                this.validateError.TeacherCode = Resource[CommonJS.languageCode].validate.teacherCodeEmpty;
                return false;
            }

            if (isNaN(teacherCode.charAt(teacherCode.length - 1))) {
                this.validateError.TeacherCode = Resource[CommonJS.languageCode].validate.teacherCodeInvalid;
                return false;
            }

            this.validateError.TeacherCode = "";
            return true;
        },

        /**
         * Method để validate Email
         * Author: LMDuc (21/11/2021)
         */
        validateEmail() {
            let email = this.teacher.Email;

            if (email && !Validate.validateEmail(email)) {
                this.validateError.Email = Resource[CommonJS.languageCode].validate.emailInvalid;
                return false;
            }

            this.validateError.Email = "";
            return true;
        },

        /**
         * Method để validate LeavingDate
         * Author: LMDuc (21/11/2021)
         */
        validateLeavingDate() {
            let leavingDate = this.teacher.LeavingDate;
            let leavingDateTime = new Date(leavingDate);
            let today = new Date();
            
            // Ngày nghỉ việc không được lớn hơn ngày hiện tại
            if (leavingDate && leavingDateTime.getTime() > today.getTime()) {
                this.validateError.LeavingDate = Resource[CommonJS.languageCode].validate.leavingDateSoMuch;
                return false;
            }

            this.validateError.LeavingDate = "";
            return true;
        }
    },

    created() {
        let me = this;

        // Lấy dữ liệu môn học, kho/phòng và tổ bộ môn
        request({
            url: '/Subjects',
            method: "GET",
        }, false)
            .then((res) => {
                let subjectOptions = res.map((item) => ( {text: item.SubjectName, value: item.SubjectId} ))
                me.subjects = subjectOptions;
            })
            .catch((error) => {
                console.log(error);
            });

        request({
            url: '/Departments',
            method: "GET",
        }, false)
            .then((res) => {
                let departmentOptions = res.map((item) => ( {text: item.DepartmentName, value: item.DepartmentId} ))
                me.departments = departmentOptions;
            })
            .catch((error) => {
                console.log(error);
            });
        
        request({
            url: '/EquipmentRooms',
            method: "GET",
        }, false)
            .then((res) => {
                let equipmentRoomOptions = res.map((item) => ( {text: item.EquipmentRoomName, value: item.EquipmentRoomId} ))
                me.equipmentRooms = equipmentRoomOptions;
            })
            .catch((error) => {
                console.log(error);
            });
    },

    watch: {
        formMode: function (formModeValue) {
            let me = this;

            // Clean error message
            me.validateError = {
                TeacherCode: "",
                FullName: "",
                Email: "",
                LeavingDate: "",
            };

            if (formModeValue === myEnum.formMode.add) {
                // Clean form khi mở form để add
                me.teacher = {
                    TeacherCode: "",
                    FullName: "",
                    PhoneNumber: "",
                    Email: "",
                    DepartmentId: "",
                    SubjectIds: [],
                    EquipmentQualification: false,
                    WorkStatus: true,
                    LeavingDate: "",
                    EquipmentRoomIds: [],
                },
                
                // Thực hiện lấy mã giáo viên mới
                request({
                    url: '/Teachers/NewTeacherCode',
                    method: "GET",
                }, false)
                    .then((res) => {
                        me.teacher.TeacherCode = res.Data;

                        // Focus vào ô nhập liệu đầu tiên
                        me.$refs.FullName.focus();
                    })
                    .catch((error) => {
                        console.log(error);
                    });
            } 
            else if (formModeValue === myEnum.formMode.edit) {
                if (!this.teacherForEdit) {
                    return;
                }

                let teacher = { ...this.teacherForEdit }

                // Xử lý dữ liệu để binding lên form
                if (teacher.LeavingDate) {
                    teacher.LeavingDate = Format.formatDate(teacher.LeavingDate, 'yyyy-mm-dd')
                }
                if (teacher.SubjectIds && typeof(teacher.SubjectIds) === 'string') {
                    teacher.SubjectIds = teacher.SubjectIds.split(',');
                }
                if (teacher.EquipmentRoomIds && typeof(teacher.EquipmentRoomIds) === 'string') {
                    teacher.EquipmentRoomIds = teacher.EquipmentRoomIds.split(',');
                }
                
                me.teacher = teacher;

                // Focus vào ô nhập liệu đầu tiên
                me.$refs.FullName.focus();
                
            }
        },

        'teacher.FullName': function() {
            this.validateFullName();
        },
        'teacher.TeacherCode': function() {
            this.validateTeacherCode();
        },
        'teacher.Email': function() {
            this.validateEmail();
        },
        'teacher.LeavingDate': function() {
            this.validateLeavingDate();
        },
    }
}
</script>

<style scope>
    @import url("../../styles/component/dialog.css");

    /* Style cho date picker riêng trang này */
    .m-form-group .mx-datepicker {
        width: 168px;
        height: 32px;
    }
</style>