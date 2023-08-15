package com.example.insurance_discounts

import MySharedPreferences
import android.content.Intent
import android.os.Bundle
import android.util.Log
import android.widget.Button
import android.widget.EditText
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import com.auth0.android.jwt.JWT
import com.example.insurance_discounts.clients.UpdateUserClient
import com.example.insurance_discounts.data.UpdateUserRequest
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.GlobalScope
import kotlinx.coroutines.launch

class UpdateUserActivity : AppCompatActivity() {
    private lateinit var firstNameEditText: EditText
    private lateinit var lastNameEditText: EditText
    private lateinit var userNameEditText: EditText
    private lateinit var phoneNumberEditText: EditText
    private lateinit var addressEditText: EditText
    private lateinit var updateButton: Button
    private lateinit var cancelUpdateButton: Button
    private lateinit var sharedPreferences: MySharedPreferences

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_update_user)

        sharedPreferences = MySharedPreferences(this)
        val token = sharedPreferences.getJwt()
        val userId = extractIdFromToken(token!!)

        firstNameEditText = findViewById(R.id.firstNameEditText)
        lastNameEditText = findViewById(R.id.lastNameEditText)
        userNameEditText = findViewById(R.id.userNameEditText)
        phoneNumberEditText = findViewById(R.id.phoneNumberEditText)
        addressEditText = findViewById(R.id.addressEditText)
        updateButton = findViewById(R.id.updateButton)
        cancelUpdateButton = findViewById(R.id.cancelUpdateButton)

        updateButton.setOnClickListener {
            val firstName = firstNameEditText.text.toString()
            val lastName = lastNameEditText.text.toString()
            val userName = userNameEditText.text.toString()
            val phoneNumber = phoneNumberEditText.text.toString()
            val address = addressEditText.text.toString()

            val request = UpdateUserRequest(
                id = userId!!, // Replace with userId if needed
                firstName = firstName,
                lastName = lastName,
                userName = userName,
                phoneNumber = phoneNumber,
                address = address
            )

            updateUser(request)
        }

        cancelUpdateButton.setOnClickListener {
            val intent = Intent(this, UserActivity::class.java)
            startActivity(intent)
        }
    }

    private fun updateUser(request: UpdateUserRequest) {
        GlobalScope.launch(Dispatchers.Main) {
            try {
                val service = UpdateUserClient.service
                service.updateUser(request)
                Toast.makeText(
                    this@UpdateUserActivity,
                    "Info updated",
                    Toast.LENGTH_SHORT
                ).show()
                val intent = Intent(this@UpdateUserActivity, UserActivity::class.java)
                startActivity(intent)
            } catch (e: Exception) {
                Toast.makeText(
                    this@UpdateUserActivity,
                    "Error updating user info",
                    Toast.LENGTH_SHORT
                ).show()
                Log.e("CategoriesActivity", "Error: ${e.message}", e)
            }
        }
    }

    private fun extractIdFromToken(token: String): String? {
        try {
            // Decode the JWT token
            val jwt = JWT(token)
            return jwt.getClaim("UserId").asString()
        } catch (e: Exception) {
            e.printStackTrace()
        }
        return null
    }
}
