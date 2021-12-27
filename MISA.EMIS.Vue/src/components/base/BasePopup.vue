<template>
  <div v-show="isShowPopup" class="m-popup">
    <div class="m-popup-modal"></div>

    <div class="m-popup-box">
        <!-- Header -->
        <div class="m-popup-header">
            <div @click="closePopup" class="m-popup-header-icon m-popup-header-icon-close icon-x m-cursor-pointer"></div>
            <div class="m-infor-title">{{ title ? title : 'Thông báo' }}</div>
        </div>

        <!-- Content -->
        <div class="m-popup-content">
            

            <div class="m-popup-content-right">
                <div class="m-popup-content-info">
                    <div class="form-info">
                        {{ msg ? msg : 'Bạn có chắc chắn muốn xóa Cán bộ giáo viên đang chọn không?' }}
                    </div>
                    
                </div>
                                    
            </div>
        </div>

        <!-- Footer -->
        <div class="m-popup-footer">
            <div class="m-popup-footer-button">
                <button v-show="iShowBtnClose" @click="closePopup" class="m-btn m-second-btn">
                    Đóng
                </button>
                <button v-show="iShowBtnNo" @click="btnNoOnclick" class="m-btn m-second-btn">
                    Không
                </button>
                <button @click="btnYesOnClick" class="m-btn m-btn-default">
                    {{ btnSaveText }}
                </button>
            </div>
        </div>
    </div>
  </div>

  
</template>

<script>
import EventBus from "../../utils/eventBus";
import Resource from '../../utils/resource';
import CommonJS from '../../utils/common';

export default {
  name: 'popup',

  data() {
    return {
      isShowPopup: false,
      iShowBtnClose: false,
      iShowBtnNo: true,
      btnSaveText: Resource[CommonJS.languageCode].delete,
      msg: "",
      title: "",
      yesCallback: () => {},
      noCallBack: () => {}
    };
  },

  methods: {
    /**
     * Method xử lý để hiển thị popup
     * Author: LMDuc (19/11/2021)
     */
    showPopupHandle(payload) {
      this.msg = payload.msg;
      this.title = payload.title;
      this.yesCallback = payload.yesCallback;
      this.iShowBtnClose = payload.iShowBtnClose ? payload.iShowBtnClose : false;
      this.btnSaveText = payload.btnSaveText ? payload.btnSaveText : Resource[CommonJS.languageCode].delete;
      if (payload.noCallBack) {
        this.noCallBack = payload.noCallBack;
      }

      if (typeof(payload.iShowBtnNo) === 'boolean') {
        this.iShowBtnNo = payload.iShowBtnNo;
      }

      this.isShowPopup = true;
    },

    /**
     * Method xử lý sự kiện để khi click vào button không
     * Author: LMDuc (19/11/2021)
     */
    btnNoOnclick() {
      if(this.noCallBack) {
        this.noCallBack();
      }
      this.isShowPopup = false;
    },

    /**
     * Method xử lý sự kiện click vào nút đóng popup
     * Author: LMDuc (19/11/2021)
     */
    closePopup() {
      this.isShowPopup = false;
    },

    /**
     * Method xử lý khi nhấn vào button yes
     * Author: LMDuc (19/11/2021)
     */
    btnYesOnClick() {
      this.yesCallback();
      this.isShowPopup = false;
    }
  },

  created() {
    // Listening the event showPopup
    EventBus.$on("showPopup", this.showPopupHandle);
  },
  destroyed() {
    // Stop listening the event showPopup with handler
    EventBus.$off("showPopup", this.showPopupHandle);
  },
};
</script>

<style scoped>
</style>