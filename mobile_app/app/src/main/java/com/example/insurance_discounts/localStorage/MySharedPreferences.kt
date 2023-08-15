import android.content.Context
import android.content.SharedPreferences

class MySharedPreferences(context: Context) {
    private val sharedPreferences: SharedPreferences = context.getSharedPreferences("MyPrefs", Context.MODE_PRIVATE)
    private val editor: SharedPreferences.Editor = sharedPreferences.edit()

    fun saveJwt(jwt: String) {
        editor.putString("jwt", jwt)
        editor.apply()
    }

    fun getJwt(): String? {
        return sharedPreferences.getString("jwt", null)
    }

    fun clearJwt() {
        editor.remove("jwt")
        editor.apply()
    }

    fun savePageId(pageId: Int) {
        editor.putInt("pageId", pageId)
        editor.apply()
    }

    fun getPageId(): Int? {
        return sharedPreferences.getInt("pageId", 0)
    }

    fun clearPageId() {
        editor.remove("pageId")
        editor.apply()
    }
}
