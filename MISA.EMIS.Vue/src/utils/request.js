import env from '../utils/env'
import axios from 'axios';
import Toastify from "./toastify";
import myEnum from "./enum";
import Resource from "./resource";
import CommonJS from "./common";

/**
 * Method thực hiện call api ở server
 * @param {Object} option {method, url, data}
 * @param {*} isShowToast Có show toast message hay không?
 * @param {*} isShowPopup Có show popup hay không?
 * @returns respone
 * Author: LMDuc (21/11/2021)
 */
export default async function request(option, isShowToast = true, isShowPopup = false) {
    option.url = env.apiUrlBackend + option.url;

    // Hiển thị nút loading
    Toastify.showLoading();

    return axios(option)
        .then((res) => {
            // Hiển thị nút loading
            Toastify.hideLoading();

            if (isShowToast) {
                if (res.status < 300) {
                    // Show toast
                    Toastify.showToastMsg({
                        msg: res.data.UserMsg ? res.data.UserMsg : Resource[CommonJS.languageCode].response.success,
                        type: myEnum.toastType.success
                    });
                } else {
                    // Show toast
                    Toastify.showToastMsg({
                        msg: res.data.UserMsg ? res.data.UserMsg : Resource[CommonJS.languageCode].response.warning,
                        type: myEnum.toastType.warning
                    });
                }
            }
            
            return res.data;
        })
        .catch((error) => {
            if (isShowPopup) {
                Toastify.showPopupMsg({
                    yesCallback: () => { },
                    title: "",
                    msg: error.response.data.UserMsg ? error.response.data.UserMsg : Resource[CommonJS.languageCode].response.error,
                    btnSaveText: Resource[CommonJS.languageCode].close,
                    iShowBtnNo : false
                })
            } else {
                Toastify.showToastMsg({
                    msg: error.response.data.UserMsg ? error.response.data.UserMsg : Resource[CommonJS.languageCode].response.error,
                    type: myEnum.toastType.error
                });
            }

            // Hiển thị nút loading
            Toastify.hideLoading();
        });
}
