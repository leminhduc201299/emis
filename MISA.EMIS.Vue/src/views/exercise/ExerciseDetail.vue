<template>
    <!-- Dialog -->
    <div class="m-dialog">
        <div class="m-dialog-modal"></div>

        <div class="m-dialog-box">
            <div class="m-dialog-box-exercise">
                <div @click="btnCloseOnClick" class="m-dialog-icon-x icon-x-2"></div>

                <div class="m-dialog-exercise-title">
                    Bổ sung thông tin
                </div>

                <div class="m-dialog-exercise-body">
                    <div class="m-dialog-exercise-body-left">

                        <div class="m-row">
                            <div class="m-form-group flex">
                                <label class="m-input-label">Ảnh đại diện</label>
                                <div 
                                    class="m-exercise-img icon-eng-2 tooltip"
                                    :class="{ 'm-input-invalid': validateError.ExerciseImg }"
                                >
                                    <img v-if="exercise.ExerciseImg" :src="`${baseFileUrl}${exercise.ExerciseImg}`"
                                        @error="onErrorExerciseImg($event, exercise.ExerciseImg)"
                                    />
                                    
                                    <div @click="btnExerciseImgOnClick" class="m-exercise-select-file icon-photo">
                                        <input 
                                            ref="ExerciseImg" 
                                            v-on:change="handleFileUpload"
                                            class="m-exercise-select-file-hide" type="file" id="ExerciseImg" accept="image/*"
                                        />
                                    </div>

                                    <span v-if="validateError.ExerciseImg" class="tooltiptext tooltip-right">{{ validateError.ExerciseImg }}</span>
                                </div>
                            </div>
                        </div>

                    </div>

                    <div class="m-dialog-exercise-body-right">
                        <div class="m-row">
                            <div class="m-form-group tooltip">
                                <label class="m-input-label">Tên bài tập <span class="m-span-require">*</span></label>
                                <input 
                                    ref="ExerciseName" 
                                    v-model="exercise.ExerciseName"
                                    class="m-input" style="width: 452px;" type="text" 
                                    :class="{ 'm-input-invalid': validateError.ExerciseName }"
                                    placeholder="Nhập tên bài tập..."
                                >
                                <span v-if="validateError.ExerciseName" class="tooltiptext tooltip-left">{{ validateError.ExerciseName }}</span>
                            </div>
                        </div>
                        
                        <div class="m-row">
                            <div class="m-form-group tooltip">
                                <label class="m-input-label">Môn học <span class="m-span-require">*</span></label>

                                <BaseComboBox
                                    :options="subjects"
                                    v-model="exercise.SubjectId"
                                    :isInvalid="validateError.SubjectId"
                                    :tabindex="5"
                                    @change="handleGradeAndSubjectOnChange"
                                />
                                <span v-if="validateError.SubjectId" class="tooltiptext tooltip-left">{{ validateError.SubjectId }}</span>
                            </div>

                            <div class="m-form-group tooltip">
                                <label class="m-input-label">Khối lớp <span class="m-span-require">*</span></label>

                                <BaseComboBox
                                    :options="grades"
                                    v-model="exercise.GradeId"
                                    :isInvalid="validateError.GradeId"
                                    :tabindex="5"
                                    @change="handleGradeAndSubjectOnChange"
                                />
                                <span v-if="validateError.GradeId" class="tooltiptext tooltip-left">{{ validateError.GradeId }}</span>
                            </div>
                        </div>

                        <div class="m-row">
                            <div class="m-form-group">
                                <label class="m-input-label">Chủ đề</label>

                                 <BaseComboBox
                                    :options="topics"
                                    v-model="exercise.TopicId"
                                    :widthValue="452"
                                    :tabindex="5"
                                />
                            </div>
                        </div>

                        <div class="m-row">
                            <div class="m-form-group">
                                <label class="m-input-label">Thẻ tìm kiếm</label>

                                <div v-if="!(searchTags && searchTags.length)" class="m-no-searchtag">Không có thẻ tìm kiếm nào</div>
                                
                                <BaseSelectTag
                                    :options="searchTags"
                                    v-model="exercise.SearchTagNames"
                                />

                            </div>
                        </div>

                    </div>
                </div>

                <div class="m-dialog-exercise-footer">
                    <button @click="btnCloseOnClick" class="m-btn m-second-btn">
                        Đóng
                    </button>

                    <button @click="btnSaveOnClick" class="m-btn m-btn-default">
                        Lưu
                    </button>
                </div>

            </div>
        </div>
    </div>
</template>

<script>
import { mapGetters } from 'vuex'
import myEnum from '../../utils/enum.js'
import BaseComboBox from '../../components/base/BaseComboBox.vue'
import BaseSelectTag from '../../components/base/BaseSelectTag.vue'
import request from "../../utils/request";
import env from '../../utils/env'
import myConstant from '../../utils/constant.js'
import Resource from "../../utils/resource";
import CommonJS from "../../utils/common";


export default {
    name: 'exercise-detail',

    components: {
        BaseComboBox,
        BaseSelectTag,
    },

    data() {
        return {
            exercise: {
                ExerciseName: "",
                SubjectId: "",
                GradeId: "",
                TopicId: "",
                SearchTagNames: [],
                ExerciseImg: "",
                ExerciseStatus: false,
                Questions: []
            },

            validateError: {
                ExerciseName: '',
                SubjectId: '',
                GradeId: '',
                ExerciseImg: '',
            },

            baseFileUrl: env.baseFileUrl,
        }
    },

    props: {
        formMode: {
            type: Number,
            default: myEnum.formMode.view,
        },

        exerciseForEdit: {
            type: Object,
            default: null,
        }
    },

    computed: {
        ...mapGetters([
            'searchTags',
            'grades',
            'subjects',
            'topics',
        ]),

        changeData() {
            const { SubjectId, GradeId } = this.exercise
            return {
                SubjectId,
                GradeId
            }
        },
    },

    methods: {
        /**
         * Sự kiện khi click vào button close
         * Author: LMDuc (09/12/2021)
         */
        btnCloseOnClick() {
            this.$emit('closeExerciseDetail')
        },

        /**
         * Sự kiện khi click vào button chọn ảnh exercise
         * Author: LMDuc (09/12/2021)
         */
        btnExerciseImgOnClick() {
            this.$refs.ExerciseImg.click();
        },

        /**
         * Handles a change on the file upload
         * Author: LMDuc (09/12/2021)
         */
        handleFileUpload() {
            let avatarFile = this.$refs.ExerciseImg.files[0];
            let fileExtension = avatarFile.name.split('.').pop().toLowerCase();

            if (!myConstant.imageFileExtension.includes(fileExtension)) {
                this.validateError.ExerciseImg = Resource[CommonJS.languageCode].validate.formatFile;
                return;
            } else {
                this.validateError.ExerciseImg = "";
            }

            // Initialize the form data
            let formData = new FormData();
            formData.append('file', avatarFile);

            request({
                method: "post",
                url: "/Exercises/UploadFile",
                data: formData,
                headers: {
                    'Content-Type': 'multipart/form-data'
                },
            }, false)
                .then((res) => {
                    this.exercise.ExerciseImg = res.Data;           
                })
                .catch((error) => {
                console.log(error);
                });
        },

        /**
         * Xử lý sự kiện load exercise lỗi
         * Author: LMDuc (22/12/2021)
         */
        onErrorExerciseImg(e, exerciseImgUrl) {
            e.target.onerror = null; 
            e.target.src = `${env.baseFileUrlTemp}${exerciseImgUrl}`
        },

        /**
         * Sự kiện khi Grade and Subjec change
         * Author: LMDuc (20/12/2021)
         */
        handleGradeAndSubjectOnChange() {   
            this.exercise.SearchTagNames = [];
            this.exercise.TopicId = '';
        },

        /**
         * Sự kiện khi click vào button save
         * Author: LMDuc (09/12/2021)
         */
        btnSaveOnClick() {
            try {
                // Validate dữ liệu
                if (!this.validateInput()) {
                    return;
                }

                let exercise = { ...this.exercise };

                if ( exercise.SearchTagNames) {
                    exercise.SearchTagNames = exercise.SearchTagNames.join('||')
                }
                
                if (this.formMode === myEnum.formMode.add) {
                    console.log('-----> ', exercise);

                    this.$store.commit('exercise/SET_EXERCISE', exercise)
                    this.$router.push({ path: '/question'})
                    this.$emit('closeExerciseDetail');
                }
                else if (this.formMode === myEnum.formMode.edit) {
                    this.$store.commit('exercise/SET_EXERCISE', exercise);
                    this.$emit('closeExerciseDetail');
                    console.log('edit')
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
            let isValid = true;

            if (!this.validateExerciseName()) {
                refElements.ExerciseName.focus();
                isValid = false;
            }

            if (!this.validateSubjectId()) {
                // refElements.TeacherCode.focus();
                isValid = false;
            }

            if (!this.validateGradeId()) {
                // refElements.Email.focus();
                isValid = false;
            }

            return isValid;
        },

        /**
         * Method để validate ExerciseName
         * Author: LMDuc (17/12/2021)
         */
        validateExerciseName() {
            if (!this.exercise.ExerciseName) {
                this.$set(this.validateError, 'ExerciseName', Resource[CommonJS.languageCode].validate.exerciseName);
                return false;
            }

            this.validateError.ExerciseName = "";
            return true;
        },

        /**
         * Method để validate SubjectId
         * Author: LMDuc (17/12/2021)
         */
        validateSubjectId() {
            if (!this.exercise.SubjectId) {
                this.$set(this.validateError, 'SubjectId', Resource[CommonJS.languageCode].validate.subject);
                return false;
            }

            this.validateError.SubjectId = "";
            return true;
        },

        /**
         * Method để validate GradeId
         * Author: LMDuc (17/12/2021)
         */
        validateGradeId() {
            if (!this.exercise.GradeId) {
                this.$set(this.validateError, 'GradeId', Resource[CommonJS.languageCode].validate.grade);
                return false;
            }

            this.validateError.GradeId = "";
            return true;
        },
    },

    created() {
  
    },

    watch: {
        formMode: function (formModeValue) {
            let me = this;

            if (formModeValue === myEnum.formMode.add) {
                // Clean form khi mở form để add
                me.exercise = {
                    ExerciseName: "",
                    SubjectId: "",
                    GradeId: "",
                    TopicId: "",
                    SearchTagNames: [],
                    ExerciseImg: "",
                    ExerciseStatus: false,
                    Questions: []
                };                

                this.$nextTick(() => {
                    // Clean error message
                    me.validateError = {
                        ExerciseName: '',
                        SubjectId: '',
                        GradeId: '',
                        ExerciseImg: '',
                    };
                    me.exerciseImgUrl = null
                })
            } 
            else if (formModeValue === myEnum.formMode.edit) {
                if (!this.exerciseForEdit) {
                    return;
                }
                // Clean error message
                me.validateError = {
                    ExerciseName: '',
                    SubjectId: '',
                    GradeId: '',
                    ExerciseImg: '',
                };
                me.exerciseImgUrl = null;

                let exercise = { ...this.exerciseForEdit }

                console.log('object', exercise)

                // Xử lý dữ liệu để binding lên form
                if (exercise.SearchTagNames && typeof(exercise.SearchTagNames) === 'string') {
                    exercise.SearchTagNames = exercise.SearchTagNames.split('||');
                } else {
                    exercise.SearchTagNames = []
                }
                
                this.exercise = exercise;
            }

            // Focus vào ô nhập liệu đầu tiên
            me.$refs.ExerciseName.focus();            
        },

        changeData: function(changeDataVal) {
            let subjectId = changeDataVal.SubjectId;
            let gradeId = changeDataVal.GradeId;

            this.$store.dispatch('searchTag/getSearchTags', { subjectId, gradeId });
            this.$store.dispatch('topic/getTopics', { subjectId, gradeId });
        },

        'exercise.ExerciseName': function() {
            this.validateExerciseName();
        },
        'exercise.SubjectId': function() {
            this.validateSubjectId();
        },
        'exercise.GradeId': function() {
            this.validateGradeId();
        },
    }
}
</script>

<style scoped>
    
</style>