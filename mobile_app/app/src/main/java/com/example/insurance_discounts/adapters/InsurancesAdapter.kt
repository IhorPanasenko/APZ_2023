package com.example.insurance_discounts.adapters

import MySharedPreferences
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import android.widget.TextView
import android.widget.Toast
import androidx.recyclerview.widget.RecyclerView
import com.example.insurance_discounts.R
import com.example.insurance_discounts.clients.UserClient
import com.example.insurance_discounts.clients.okHttpClient
import com.example.insurance_discounts.data.Insurance
import com.example.insurance_discounts.data.UserPolicyRequest
import com.example.insurance_discounts.databinding.ItemInsuranceBinding
import com.example.insurance_discounts.services.UserService
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.GlobalScope
import kotlinx.coroutines.launch
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory
import java.text.SimpleDateFormat
import java.util.Date
import java.util.Locale

class InsurancesAdapter(private val userId: String) :
    RecyclerView.Adapter<InsurancesAdapter.InsuranceViewHolder>() {
    private val insurances: MutableList<Insurance> = mutableListOf()
    private val dateFormat = SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss.SSS'Z'", Locale.getDefault())

    fun setInsurances(insurances: List<Insurance>) {
        this.insurances.clear()
        this.insurances.addAll(insurances)
        println(insurances)
        notifyDataSetChanged()
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): InsuranceViewHolder {
        val view =
            LayoutInflater.from(parent.context).inflate(R.layout.item_insurance, parent, false)
        return InsuranceViewHolder(view)
    }

    override fun onBindViewHolder(holder: InsuranceViewHolder, position: Int) {
        println(position)
        val insurance = insurances[position]
        holder.bind(insurance)
    }

    override fun getItemCount(): Int {
        return insurances.size
    }

    inner class InsuranceViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {
        private val nameTextView: TextView = itemView.findViewById(R.id.nameTextView)
        private val descriptionTextView: TextView = itemView.findViewById(R.id.descriptionTextView)
        private val companyTextView: TextView = itemView.findViewById(R.id.companyTextView)
        private val buyButton: Button = itemView.findViewById(R.id.buyButton)
        private val detailsButton: Button = itemView.findViewById(R.id.detailsButton)
        private lateinit var sharedPreferences: MySharedPreferences

        fun bind(insurance: Insurance) {
            nameTextView.text = insurance.name
            descriptionTextView.text = insurance.description
            companyTextView.text = insurance.company.companyName


            // Handle buy button click
            buyButton.setOnClickListener {
                val policyId = insurance.id

                val currentDate = Date()
                val startDate = dateFormat.format(currentDate)
                val endDate = dateFormat.format(currentDate)
                // Handle buy button click event

                val requestBody = UserPolicyRequest(
                    startDate = startDate,
                    endDate = endDate,
                    policyId = policyId,
                    userId = userId
                )

                sendPostRequest(requestBody)
            }

            detailsButton.setOnClickListener {
                println("Button details clicked")
            }
        }
    }

    private fun sendPostRequest(requestBody: UserPolicyRequest) {
        println(requestBody)

        GlobalScope.launch(Dispatchers.Main) {
            try {
                val service = UserClient.create()
                service.createUserPolicy(requestBody)

            } catch (e: Exception) {
                Log.e("CategoriesActivity", "Error: ${e.message}", e)
            }
        }
    }

}
