<template>
  <div 
    v-show="isShowToastMessage" 
    @click="closeToastMsg" 
    :class="{'m-toast-warning': type == myEnum.toastType.warning,
      'm-toast-error': type == myEnum.toastType.error
    }"
    class="m-toast m-cursor-pointer"
  >
    <div 
      class="m-toast-icon-left icon-toast-success"
      :class="{'icon-toast-warning': type == myEnum.toastType.warning,
        'icon-toast-error': type == myEnum.toastType.error
      }"
    >
    </div>
    <div class="m-toast-content">
      <div class="m-toast-title">{{ titleMsg }}</div>
      <div class="m-toast-text">{{ msg }}</div>
    </div>
  </div>
</template>

<script>
import EventBus from '../../utils/eventBus'
import myEnum from '../../utils/enum'
import Resource from '../../utils/resource';
import CommonJS from '../../utils/common';

export default {
  name: "toast-message",

  data() {
    return {
      isShowToastMessage: false,
      msg: "",
      type: myEnum.toastType.success,
      myEnum: myEnum
    }
  },

  methods: {
    /**
     * Method xử lý để hiển thị toast message
     * Author: LMDuc (21/11/2021)
     */
    showToastMsgHandle(option) {
      this.msg = option.msg;
      this.type = option.type;
      
      this.isShowToastMessage = true;

      setTimeout(() => {
        this.isShowToastMessage = false;
      }, 3000);
    },
    
    /**
     * Method để đóng toast message
     * Author: LMDuc (21/11/2021)
     */
    closeToastMsg() {
      this.isShowToastMessage = false;
    },
  },

  computed: {
    /**
     * Tính toán title để hiển thị
     * Author: LMDuc (21/11/2021)
     */
    titleMsg: function() {
      let toastType = this.type;

      if (toastType == myEnum.toastType.success) {
        return Resource[CommonJS.languageCode].toastType.success;
      } else if (toastType == myEnum.toastType.warning) {
        return Resource[CommonJS.languageCode].toastType.warning;
      } else if (toastType == myEnum.toastType.error) {
        return Resource[CommonJS.languageCode].toastType.error;
      }

      return "";
    }
  },

  created() {
    // Listening the event showToastMsg
    EventBus.$on("showToastMsg", this.showToastMsgHandle);
  },
  destroyed() {
    // Stop listening the event showToastMsg with handler
    EventBus.$off("showToastMsg", this.showToastMsgHandle);
  },
};
</script>

<style scoped>
</style>