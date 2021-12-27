import Resource from './resource';

class CommonJS {
    // Mã ngôn ngữ
    static languageCode = 'VN';

    /**
     * Set mã ngôn ngữ
     * Author: LMDuc (18/11/2021)
     * @param {String} languageCode Mã ngôn ngữ
     */
    static setLanguageCode(languageCode) {
        if (!Resource[languageCode]) {
            languageCode = 'EN';
        }

        CommonJS.languageCode = languageCode;
    }

}

export default CommonJS;