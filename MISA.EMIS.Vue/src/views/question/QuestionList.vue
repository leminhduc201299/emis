<template>
  <div>
    <!-- Header -->
    <div class="question-header">
      <div class="header-name">
          <div
            @click="onBtnBackClick"
            class="header-icon icon-back m-cursor-pointer"
          ></div>
          <div class="header-logo icon-english-logo m-cursor-pointer">
            <img v-if="exercise && exercise.ExerciseImg" :src="`${baseFileUrl}${exercise.ExerciseImg}`" @error="onErrorExerciseImg($event, exercise.ExerciseImg)" />
          </div>

          <input 
            type="text" class="header-text" placeholder="Nhập tên bài tập..." 
            v-model="exerciseName"
          >
      </div>

      <div class="question-header-tool">
        <div class="question-header-tool-left">
          <BaseComboBox
            :options="subjects"
            v-model="subjectId"
            placeholder=""
            :tabindex="5"
            :widthValue="160"
          />

          <BaseComboBox
            :options="grades"
            v-model="gradeId"
            placeholder=""
            :tabindex="5"
            :widthValue="100"
          />

          <button @click="showExerciseDetail(myEnum.formMode.edit, exercise)" class="m-btn m-second-btn">Bổ sung thông tin</button>
        </div>

        <div class="question-header-tool-right">
          <button @click="btnCompleteExerciseOnClick" class="m-btn m-btn-default">Hoàn thành</button>
        </div>
      </div>
    </div>

    <!-- Content -->
    <div class="question-content">

      <keep-alive>
          <component v-bind:is="currentTabComponent"></component>
      </keep-alive>
      <!-- <QuestionListEmpty /> -->

      
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
import { mapGetters } from 'vuex'
import BaseComboBox from "../../components/base/BaseComboBox.vue";
import myEnum from "../../utils/enum.js";
import ExerciseDetail from "../exercise/ExerciseDetail.vue";
import QuestionListEmpty from "./component/QuestionListEmpty.vue";
import QuestionListContent from "./component/QuestionListContent.vue";
import env from '../../utils/env'

export default {
  name: "question-list",

  components: {
    BaseComboBox,
    ExerciseDetail,
    QuestionListEmpty,
    QuestionListContent,
  },

  data() {
    return {
      myEnum: myEnum,
      formMode: myEnum.formMode.view,
      isShowExerciseDetail: false,
      exerciseForEdit: null,

      baseFileUrl: env.baseFileUrl,
    };
  },

  computed: {
    ...mapGetters([
      'exercise',
      'grades',
      'subjects',
      'topics',
    ]),

    exerciseName: {
      get() {
        return this.exercise ? this.exercise.ExerciseName : '';
      },
      set(value) {
        this.$store.commit('exercise/SET_EXERCISE_ATTRIBUTE', { key: 'ExerciseName', value: value })
      }
    },

    subjectId: {
      get() {
        return this.exercise ? this.exercise.SubjectId : '';
      },
      set(value) {
        this.$store.commit('exercise/SET_EXERCISE_ATTRIBUTE', { key: 'SubjectId', value: value })
      }
    },

    gradeId: {
      get() {
        return this.exercise ? this.exercise.GradeId : '';
      },
      set(value) {
        this.$store.commit('exercise/SET_EXERCISE_ATTRIBUTE', { key: 'GradeId', value: value })
      }
    },

    currentTabComponent() {
      let exercise = this.exercise;

      if (exercise && exercise.Questions && exercise.Questions.length > 0) {
        return 'QuestionListContent'
      } else {
        return 'QuestionListEmpty'
      }
    }
  },

  methods: {
    /**
     * Method mở dialog exercise theo form mode
     * Author: LMDuc (09/12/2021)
     */
    onBtnBackClick() {
      this.$router.go(-1);
    },

    /**
     * Method mở dialog question detail theo form mode
     * Author: LMDuc (11/12/2021)
     */
    showQuestionDetail(formMode, question, exerciseId, questionType) {
      this.formMode = formMode;
      this.questionForEdit = question;
      this.exerciseId = exerciseId;
      this.questionType = questionType;

      this.isShowQuestionDetail = true;
    },

    /**
     * Method để đóng dialog thông tin question
     * Author: LMDuc (11/12/2021)
     */
    closeQuestionDetail() {
      this.formMode = myEnum.formMode.view;
      this.isShowQuestionDetail = false;
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
    },

    /**
     * Method xủ lý sự kiện khi click vào button hoàn thành
     * Author: LMDuc (20/12/2021)
     */
    btnCompleteExerciseOnClick() {
      this.$store.dispatch('exercise/completeExercise');
      this.$router.go(-1);
    },

    /**
     * Xử lý sự kiện load exercise lỗi
     * Author: LMDuc (22/12/2021)
     */
    onErrorExerciseImg(e, exerciseImgUrl) {
        e.target.onerror = null; 
        e.target.src = `${env.baseFileUrlTemp}${exerciseImgUrl}`
    },
        
  },

  created() {
  },

  watch: {
    exercise: {
      immediate: true,

      handler(exerciseVal) {
        if (!exerciseVal) {
          if (this.$route.query.exerciseId) {
            this.$store.dispatch('exercise/getExerciseById', this.$route.query.exerciseId)

            // Lấy dữ liệu grade, subject, topic
            this.$store.dispatch('grade/getGrades');
            this.$store.dispatch('subject/getSubjects');
          }
          else {
            this.$router.go(-1);
          }
        }
      }
    },

  }
};
</script>

<style scoped>
@import url("../../styles/common/question-header.css");
@import url("../../styles/common/question-content.css");
</style>