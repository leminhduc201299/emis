import EventBus from './eventBus'

class Toastify {
    /**
     * Method để show toast
     * @param {String} msg mesage
     * Author: LMDuc (19/11/2021)
     */
    static showToastMsg(msg) {
        // Send the event on a channel (showToastMsg) with a payload
        EventBus.$emit('showToastMsg', msg);
    }

    /**
     * Method để hiển thị popup
     * @param {Object} payload {iShowBtnClose, btnSaveText, msg, title, yesCallback, noCallBack}
     * Author: LMDuc (19/11/2021)
     */
    static showPopupMsg(payload) {
        // Send the event on a channel (showPopup) with a payload
        EventBus.$emit('showPopup', payload);
    }

    /**
     * Method để hiển thị button loading
     * Author: LMDuc (19/11/2021)
     */
     static showLoading() {
        // Send the event on a channel (showLoading)
        EventBus.$emit('showLoading');
    }

    /**
     * Method để hide button loading
     * Author: LMDuc (19/11/2021)
     */
     static hideLoading() {
        // Send the event on a channel (hideLoading)
        EventBus.$emit('hideLoading');
    }
}

export default Toastify;