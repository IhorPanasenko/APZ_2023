package com.example.insurance_discounts

import MySharedPreferences
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.content.Intent
import android.widget.Toast
import com.example.insurance_discounts.R
import com.example.insurance_discounts.clients.LoginClient
import com.example.insurance_discounts.databinding.LoginBinding
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.GlobalScope
import kotlinx.coroutines.launch
import kotlinx.coroutines.withContext

import com.auth0.android.jwt.JWT



class MainActivity : AppCompatActivity() {
    private lateinit var binding: LoginBinding
    private val loginClient = LoginClient()
    private lateinit var sharedPreferences: MySharedPreferences


    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = LoginBinding.inflate(layoutInflater)
        val view = binding.root
        setContentView(view)

        sharedPreferences = MySharedPreferences(this)

        binding.registerLinkTextView.setOnClickListener {
            navigateToRegisterActivity()
        }

        binding.loginButton.setOnClickListener {
            val email = binding.emailEditText.text.toString()
            val password = binding.passwordEditText.text.toString()

            if (email.isNotEmpty() && password.isNotEmpty()) {
                GlobalScope.launch(Dispatchers.Main) {
                    val response = withContext(Dispatchers.IO) {
                        loginClient.login(email, password)
                    }

                    if (response.isSuccessful) {
                        val loginResponse = response.body()
                        println(loginResponse)
                        if(loginResponse!=null){
                            val userRole = extractRoleFromToken(loginResponse.toString())
                            println(userRole)
                            if(userRole == "User"){
                                Toast.makeText(this@MainActivity, "Login successful", Toast.LENGTH_SHORT)
                                    .show()
                                sharedPreferences.saveJwt(loginResponse);
                                sharedPreferences.savePageId(0);
                                val intent = Intent(this@MainActivity, MainPageActivity::class.java)
                                startActivity(intent)
                                println(sharedPreferences.getJwt())
                            }
                            else{
                                Toast.makeText(this@MainActivity, "Only general users can have access to the mobile app", Toast.LENGTH_SHORT).show()
                            }
                        }

                        // Handle successful login, e.g., navigate to the main page
                    } else {
                        Toast.makeText(this@MainActivity, "Login failed", Toast.LENGTH_SHORT).show()
                        // Handle login failure, e.g., display an error message
                    }
                }
            } else {
                Toast.makeText(
                    this@MainActivity,
                    "Please enter email and password",
                    Toast.LENGTH_SHORT
                ).show()
            }
        }
    }

    private fun navigateToRegisterActivity() {
        val intent = Intent(this, RegisterActivity::class.java)
        startActivity(intent)
    }

    private fun extractRoleFromToken(token: String): String? {
        try {
            // Decode the JWT token
            val jwt = JWT(token)
            return jwt.getClaim("Role").asString()
        } catch (e: Exception) {
            e.printStackTrace()
        }
        return null
    }
}
