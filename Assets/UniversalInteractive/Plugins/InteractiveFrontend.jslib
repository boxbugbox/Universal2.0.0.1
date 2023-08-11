mergeInto(LibraryManager.library, {

  CreateWord: function (str) {
    SetData(UTF8ToString(str));
  },

  SendData: function (str) {
    SetResult(UTF8ToString(str));
  },
  GetUrldata:function () {
    UrlSearch();
  },
});