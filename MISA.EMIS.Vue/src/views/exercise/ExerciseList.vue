<template>
    <div>
        <!-- Header -->
        <div class="header">
            <div class="header-left">
                <div class="header-left-navitem">
                    <div class="header-left-navitem-icon icon-dashboard"></div>

                    <div class="header-left-navitem-text">
                        Tổng quan
                    </div>
                </div>

                <div class="header-left-navitem">
                    <div class="header-left-navitem-icon icon-class"></div>

                    <div class="header-left-navitem-text">
                        Lớp học
                    </div>
                </div>

                <div class="header-left-navitem navitem-active">
                    <div class="header-left-navitem-icon icon-document"></div>

                    <div class="header-left-navitem-text">
                        Học liệu
                    </div>
                </div>

            </div>

            <div class="header-right">
                <div class="header-right-item header-right-icon icon-toggle m-cursor-pointer"></div>
                <div class="header-right-item header-right-icon icon-setting m-cursor-pointer"></div>
                <div class="header-right-item header-right-icon icon-chat m-cursor-pointer"></div>
                <div class="header-right-item header-right-icon icon-notifi m-cursor-pointer">
                    <div class="notifi-number">2</div>
                </div>

                <div class="header-right-item header-right-name m-cursor-pointer">Đức</div>

                <div class="header-right-item header-right-avatar icon-avatar m-cursor-pointer"></div>

            </div>
        </div>

        <!-- Content -->
        <div class="content">
            <div class="search-bar">
                <div class="search-left">
                    <button style="margin-left: 0;" class="m-btn m-btn-purple import-content-btn">
                        Của tôi ({{ totalRecord }})
                    </button>

                    <button style="background-color: unset; color: #62768e;" class="m-btn m-btn-purple import-content-btn">
                        Chia sẻ của trường (19)
                    </button>

                    <button style="background-color: unset; color: #62768e;" class="m-btn m-btn-purple import-content-btn">
                        Tham khảo (19)
                    </button>
                </div>

                <div class="search-right">
                    <button @click="showExerciseDetail(myEnum.formMode.add, null)" class="m-btn m-btn-default">
                        Soạn bài
                    </button>
                </div>

            </div> 

            <div class="search-bar">
                <div class="search-left">
                    <BaseComboBox
                        :options="grades"
                        placeholder='Tất cả khối lớp'
                        :widthValue="172"
                        v-model="keySearch.gradeId"
                    />

                    <BaseComboBox
                        :options="subjects"
                        placeholder='Tất cả môn học'
                        :widthValue="172"
                        v-model="keySearch.subjectId"
                    />

                    <BaseComboBox
                        :options="topics"
                        placeholder='Tất cả chủ đề'
                        :widthValue="256"
                        v-model="keySearch.topicId"
                    />

                    <BaseComboBox
                        :options="exerciseStatus"
                        placeholder='Tất cả học liệu'
                        :widthValue="256"
                        v-model="keySearch.exerciseStatus"
                    />
                    
                </div>
        
                <div class="search-right">
                    <input @keyup.enter="onEnterSearchText($event)" class="m-input m-input-icon icon-search" style="width: 300px;" type="text" placeholder="Nhập tên môn học để tìm kiếm">
                </div>
            </div>


            <div class="gird">
                <div 
                    v-for="(exercise, index) in exercises" :key="index" 
                    class="m-exercise-card"
                >
                    <div 
                        class="m-exercise-img"
                        :class="`icon-${exercise && exercise.Subject && exercise.Subject.length ? exercise.Subject[0].SubjectCode: 'default'}`"
                    >
                        <img 
                            v-if="exercise.ExerciseImg"
                            :src="`${baseFileUrl}${exercise.ExerciseImg}`"
                        />
                        <div class="m-exercise-subject">
                            {{ exercise && exercise.Subject && exercise.Subject.length ? exercise.Subject[0].SubjectName : null 
                                | formatExerciseTitle(exercise && exercise.Grade && exercise.Grade.length ? exercise.Grade[0].GradeName : null) }}
                        </div>
                    </div>

                    <div class="m-exercise-info">
                        <div class="m-exercise-info-top">
                            <div class="m-exercise-name">{{ exercise ? exercise.ExerciseName : '' }}</div>
                            <div @click="btnShowExerciseActionOnClick(exercise ? exercise.ExerciseId : null)" class="m-exercise-info-top-right icon-more">
                                <div 
                                    v-show="exerciseListFocus && exerciseListFocus.length ? exerciseListFocus.includes(exercise ? exercise.ExerciseId : null) : false" 
                                    class="m-more m-cursor-pointer"
                                >
                                    <div @click="btnViewExerciseOnClick(exercise)" class="m-more-item">
                                        <div class="m-more-text">Xem bài tập</div>
                                    </div>

                                    <div @click="btnDeleteExerciseOnClick(exercise ? exercise.ExerciseId : null)" class="m-more-item">
                                        <div class="m-more-text">Xóa bài tập</div>
                                    </div>

                                </div>
                            </div>
                                
                        </div>
                        
                        <div class="m-exercise-info-bottom">
                            <div class="m-exercise-info-bottom-length">
                                <div class="m-exercise-info-bottom-icon icon-hoclieu"></div>
                                <div class="m-exercise-description">
                                    {{ exercise && exercise.Questions ? exercise.Questions.length : 0 }} câu
                                </div>
                            </div>
                            
                            <div 
                                class="m-exercise-info-bottom-status"
                                :class="{ 
                                    'm-exercise-info-bottom-status-unfinished': exercise && exercise.ExerciseStatus == 0,
                                    'm-exercise-info-bottom-status-finished': exercise && exercise.ExerciseStatus == 1
                                }"
                            >
                                {{ exercise ? exercise.ExerciseStatus : 0 | formatExerciseStatus }}
                            </div>
                        </div>
                        
                    </div>
                </div>

                <div v-if="!(exercises && exercises.length)" class="m-gird-nocontent">Không có bài tập</div>

            </div>
        </div>

        <!-- Phần footer -->
        <div class="footer">
            <div class="footer-left">
                Tổng {{ totalRecord }} học liệu
            </div>
        </div>

        <!-- Dialog Exercise Info -->
        <ExerciseDetail 
            v-show="isShowExerciseDetail" 
            :formMode="formMode"
            :exerciseForEdit="exerciseForEdit"
            @closeExerciseDetail="closeExerciseDetail"
        />    

    </div>
</template>

<script>
import ExerciseDetail from "./ExerciseDetail.vue";
import myEnum from "../../utils/enum.js";
import BaseComboBox from "../../components/base/BaseComboBox";
import Toastify from "../../utils/toastify";
import env from '../../utils/env'
import { mapGetters } from 'vuex'
import Resource from "../../utils/resource";
import CommonJS from "../../utils/common";

export default {
    name: 'exercise-list',

    data() {
        return {
            isShowExerciseDetail: false,
            formMode: myEnum.formMode.view,
            exerciseForEdit: null,
            myEnum: myEnum,

            exerciseStatus: [{ text: 'Tất cả học liệu', value: '' }, { text: 'Đang soạn', value: false }, { text: 'Đã soạn', value: true }],

            exerciseListFocus: [],

            keySearch: {
                gradeId: '',
                subjectId: '',
                topicId: '',
                exerciseStatus: '',
                searchText: '',
                pageSize: 50,
                pageNumber: 1
            },

            baseFileUrl: env.baseFileUrl,
        }
    },

    components: {
        ExerciseDetail,
        BaseComboBox,
    },

    computed: {
        ...mapGetters([
            'exercises',
            'totalRecord',
        ]),

        grades() {
            let gradeList = this.$store.getters.grades;
            return [
                { text: "Tất cả khối lớp", value: '' },
                ...gradeList
            ]
        },

        subjects() {
            let subjectList = this.$store.getters.subjects;
            return [
                { text: "Tất cả môn học", value: '' },
                ...subjectList
            ]
        },

        topics() {
            let topicList = this.$store.getters.topics;
            return [
                { text: "Tất cả chủ đề", value: '' },
                ...topicList
            ]
        },

        changeData() {
            const { subjectId, gradeId } = this.keySearch
            return {
                subjectId,
                gradeId
            }
        },
    },

    methods: {
        /**
         * Method để load bài tập trong CSDL
         * Author: LMDuc (10/12/2021)
         */
        loadData() {
            let keysearch = this.keySearch;
            
            this.$store.dispatch('exercise/getExercises', keysearch)
        },

        /**
         * Sự kiện khi nhấn enter vào ô text search
         * Author: LMDuc (16/12/2021)
         */
        onEnterSearchText(event) {
            let value = event.target.value;

            // Chuyển text search sang dạng viết thường và xóa khoảng trắng ở đầu và cuối
            value = value.toLowerCase().trim();

            this.keySearch.pageNumber = 1;
            this.keySearch.searchText = value;
        },

        /**
         * Method mở dialog exercise theo form mode
         * Author: LMDuc (09/12/2021)
         */
        showExerciseDetail(formMode, exercise) {
            this.formMode = formMode;
            this.exerciseForEdit = exercise;

            this.isShowExerciseDetail = true;
        },

        /**
         * Method để đóng dialog thông tin exercise
         * Author: LMDuc (09/12/2021)
         */
        closeExerciseDetail() {
            this.formMode = myEnum.formMode.view;
            this.isShowExerciseDetail = false;

            this.$store.dispatch('topic/getTopics', { subjectId: this.keySearch.subjectId, gradeId: this.keySearch.gradeId });
        },

        // onBtnPrepareLessonClick() {
        //     this.$router.push({ path: '/question' })
        // }

        /**
         * Xử lý sự kiên khi nhấn vào nút xóa bài tập
         * Author: LMDuc (17/12/2021)
         */
        btnDeleteExerciseOnClick(exerciseId) {
            if (exerciseId) {
                // Hiển thị cảnh báo cho ngươi dùng biết
                Toastify.showPopupMsg({
                    yesCallback: () => this.deleteQuestionHandle(exerciseId),
                    title: "",
                    iShowBtnNo: true,
                    msg: Resource[CommonJS.languageCode].deleteExercise.deleteConfirm,
                });

                
            } else {
                console.log();
            }
        },

        /**
         * Xóa exercise trong CSDL
         * Author: LMDuc (17/12/2021)
         */
        deleteQuestionHandle(exerciseId) {
            this.$store.dispatch('exercise/deleteExercise', exerciseId)
        },

        /**
         * Xử lý sự kiện khi click vào icon more
         * Author: LMDuc (11/12/2021)
         */
        btnShowExerciseActionOnClick(exerciseId) {
            if (this.exerciseListFocus.includes(exerciseId)) {
                this.exerciseListFocus = this.exerciseListFocus.filter(itemSelectd => itemSelectd !== exerciseId)
            } else {
                this.exerciseListFocus.push(exerciseId);
            }
        },

        btnViewExerciseOnClick(exercise) {
            this.$store.commit('exercise/SET_EXERCISE', exercise)
            this.$router.push({ path: '/question', query: { exerciseId: exercise.ExerciseId } })
        }
        
    },

    created() {
        let me = this;

        // Load exercises
        me.loadData();

        // Lấy dữ liệu grade, subject, topic
        this.$store.dispatch('grade/getGrades');
        this.$store.dispatch('subject/getSubjects');
    },

    filters: {
        // Xử lý dữ liệu title bài tập
        formatExerciseTitle: function (subject, grade) {
            let classNumber = '';

            if (grade) {
                classNumber = grade.match(/(\d+)/)[0];
            }

            if (!subject) {
                subject = '';
            }

            return `${subject} ${classNumber}`
        },

        formatExerciseStatus: function(status) {
            let statusString = '';

            if (status == 0) {
                statusString = "Đang soạn";
            } else if (status == 1) {
                statusString = "Đã soạn";
            }

            return statusString;
        }
    },

    watch: {
        // Khi key search thay đổi thì load lại data
        keySearch: {
            handler() {
                this.loadData();
            },

            deep: true,
        },

        changeData: {
            immediate: true,

            handler(changeDataVal) {
                let subjectId = changeDataVal.subjectId;
                let gradeId = changeDataVal.gradeId;

                this.$store.dispatch('topic/getTopics', { subjectId, gradeId });
            }
        },
    },
}
</script>

<style scoped>
    @import url('../../styles/common/header.css');
    @import url('../../styles/common/content.css');
    @import url('../../styles/common/footer.css');
</style>