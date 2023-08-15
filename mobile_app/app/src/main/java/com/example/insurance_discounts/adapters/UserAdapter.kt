package com.example.insurance_discounts.adapters

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import com.example.insurance_discounts.R
import com.example.insurance_discounts.data.UserInsurance

class UserAdapter() : RecyclerView.Adapter<UserAdapter.UserViewHolder>() {
    private val userInsurances: MutableList<UserInsurance> = mutableListOf()

    fun setUserInsurances(userInsurances: List<UserInsurance>){
        this.userInsurances.clear()
        this.userInsurances.addAll(userInsurances)
        notifyDataSetChanged()
    }

    class UserViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {
        val startDateTextView: TextView = itemView.findViewById(R.id.startDateTextView)
        val endDateTextView: TextView = itemView.findViewById(R.id.endDateTextView)
        val insuranceNameTextView: TextView = itemView.findViewById(R.id.insuranceNameTextView)
        val coverageAmountTextView: TextView = itemView.findViewById(R.id.coverageAmountTextView)
        val priceTextView: TextView = itemView.findViewById(R.id.priceTextView)
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): UserViewHolder {
        val itemView = LayoutInflater.from(parent.context).inflate(R.layout.item_user_info, parent, false)
        return UserViewHolder(itemView)
    }

    override fun onBindViewHolder(holder: UserViewHolder, position: Int) {
        val userInsurance = userInsurances[position]

        holder.startDateTextView.text = "Start Date: ${userInsurance.startDate}"
        holder.endDateTextView.text = "End Date: ${userInsurance.endDate}"
        holder.insuranceNameTextView.text = "Insurance Name: ${userInsurance.policy.name}"
        holder.coverageAmountTextView.text = "Coverage Amount: ${userInsurance.policy.coverageAmount}"
        holder.priceTextView.text = "Price: ${userInsurance.policy.price}"
    }

    override fun getItemCount(): Int {
        return userInsurances.size
    }
}
