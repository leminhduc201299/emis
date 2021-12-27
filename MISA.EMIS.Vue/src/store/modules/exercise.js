import request from "../../utils/request";
import myEnum from "../../utils/enum";
import Toastify from "../../utils/toastify";
import Resource from "../../utils/resource";
import CommonJS from "../../utils/common";

const state = {
    exercises: [],
    exercise: null,
    totalRecord: 0,
}
  
const mutations = {
    SET_EXERCISES: (state, exercises) => {
        state.exercises = exercises
    },

    SET_TOTAL_RECORD: (state, totalRecord) => {
        state.totalRecord = totalRecord
    },

    DELETE_EXERCISE: (state, exerciseId) => {
        state.exercises = state.exercises.filter(exercise => exercise.ExerciseId !== exerciseId);
    },

    SET_EXERCISE: (state, exercise) => {
      state.exercise = exercise;
    },

    SET_EXERCISE_ATTRIBUTE: (state, payload) => {
      let { key, value } = payload;
      state.exercise[key] = value;
    },
}

const actions = {
    // Get exercises
    getExercises({ commit }, keySearch) {
        const { gradeId, subjectId, topicId, exerciseStatus, searchText, pageSize, pageNumber } = keySearch;

        console.log(keySearch);

        request(
            {
              url: `/Exercises/Filter?gradeId=${gradeId}&subjectId=${subjectId}&topicId=${topicId}&exerciseStatus=${exerciseStatus}&searchText=${searchText}&pageSize=${pageSize}&pageNumber=${pageNumber}`,
              method: "GET",
            }, false
          )
            .then((res) => {
              commit('SET_EXERCISES', res.Data);
              commit('SET_TOTAL_RECORD', res.TotalRecord)
            })
            .catch((error) => {
              console.log(error);
            });
    },

    getExerciseById({ commit }, exerciseId) {
        request(
            {
              url: `/Exercises/${exerciseId}`,
              method: "GET",
            }, false
          )
            .then((res) => {
                commit('SET_EXERCISE', res)
            })
            .catch((error) => {
              console.log(error);
            });
    },

    deleteExercise({ commit }, exerciseId) {
        request(
            {
              url: `/Exercises/${exerciseId}`,
              method: "DELETE",
            }, true
          )
            .then((res) => {
                commit('DELETE_EXERCISE', exerciseId)
                console.log(res)
            })
            .catch((error) => {
              console.log(error);
            });
    },

    completeExercise({ state, dispatch }) {
      let exercise = JSON.parse(JSON.stringify(state.exercise));
      exercise.ExerciseStatus = true;

      dispatch('upsertExercise', exercise).then(res => {
        console.log(res)
        // Show toast
        Toastify.showToastMsg({
            msg: Resource[CommonJS.languageCode].completeExercise.success,
            type: myEnum.toastType.success
        });
      })
    },

    upsertExercise({ commit }, payload) {
      return new Promise((resolve, reject) => {
        request(
          {
            url: `/Exercises`,
            method: "POST",
            data: payload
          }, false
        )
          .then((res) => {
              commit('SET_EXERCISE', res.Data);

              resolve()
          })
          .catch((error) => {
            console.log(error);
            reject(error)
          });
      })
    },

    addExerciseQuestion({ state, dispatch }, payload) {
      let exercise = JSON.parse(JSON.stringify(state.exercise));
      if (exercise.Questions) {
        exercise.Questions.push(payload);
      }
      exercise.ExerciseStatus = false;

      dispatch('upsertExercise', exercise).then(res => {
        console.log(res)
        // Show toast
        Toastify.showToastMsg({
            msg: Resource[CommonJS.languageCode].addQuestion.addSuccess,
            type: myEnum.toastType.success
        });
      })
      .catch(error => {
        console.log(error)
      });
    },

    updateExerciseQuestion({ state, dispatch }, payload) {
      let exercise = JSON.parse(JSON.stringify(state.exercise));
      const { questionIndex, question} = payload;

      if (exercise.Questions) {
        exercise.Questions[questionIndex] = question;
      }
      exercise.ExerciseStatus = false;

      dispatch('upsertExercise', exercise).then(res => {
        console.log(res)
        // Show toast
        Toastify.showToastMsg({
            msg: Resource[CommonJS.languageCode].editQuestion.editSuccess,
            type: myEnum.toastType.success
        });
      })
      .catch(error => {
        console.log(error)
      });
    },

    deleteExerciseQuestion({ state, dispatch }, payload) {
      let exercise = JSON.parse(JSON.stringify(state.exercise));
      if (payload !== null && payload !== undefined) {
        exercise.Questions = exercise.Questions.filter((question, index) => index !== payload);
      }
      exercise.ExerciseStatus = false;

      dispatch('upsertExercise', exercise).then(res => {
        console.log(res)
        // Show toast
        Toastify.showToastMsg({
            msg: Resource[CommonJS.languageCode].deleteQuestion.deleteSuccess,
            type: myEnum.toastType.success
        });
      })
      .catch(error => {
        console.log(error)
      });
    },
}

export default {
    namespaced: true,
    state,
    mutations,
    actions
}
