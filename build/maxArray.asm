	sll $t3,$s3,2		# $t3=4*n	
	add $t7,$t3,$s0		# $t7: Adresse des ersten Worts nach Array A, $t7=&A[n]
loop:	beq $s0,$t7,end		# Ende, falls $s0 �ber A hinausl�uft
	lw $t1,0($s1) 		# B[i] in $t1 laden
	lw $t2,0($s2)		# C[i] in $t2 laden
	slt $t3,$t1,$t2 	# wenn $t1<$t2
	bne $t3,$zero,nimm2 	# springe zu nimm2
	sw $t1,0($s0) 		# A[i]=B[i] ($t1 an Speicherstelle $s0=&A[i] schreiben)
	j next			# �berspringe "else"
	next:	addi $s0,$s0,4		# Adresse von n�chstem Element von Array A, $s0=&A[i+1]
	addi $s1,$s1,4		# Adresse von n�chstem Element von Array B, $s1=&B[i+1]
	addi $s2,$s2,4		# Adresse von n�chstem Element von Array C, $s2=&C[i+1]
	j loop			# in die n�chste Schleife springen
	
	loop:	beq $s0,$t7,end		# Ende, falls $s0 �ber A hinausl�uft
	lw $t1,0($s1) 		# B[i] in $t1 laden
	lw $t2,0($s2)		# C[i] in $t2 laden
	slt $t3,$t1,$t2 	# wenn $t1<$t2
	bne $t3,$zero,nimm2 	# springe zu nimm2
nimm2:	sw $t2,0($s0)		# A[i]=C[i]($t2 an Speicherstelle $s0=&A[i] schreiben)
next:	addi $s0,$s0,4		# Adresse von n�chstem Element von Array A, $s0=&A[i+1]
	addi $s1,$s1,4		# Adresse von n�chstem Element von Array B, $s1=&B[i+1]
	addi $s2,$s2,4		# Adresse von n�chstem Element von Array C, $s2=&C[i+1]
	j loop			# in die n�chste Schleife springen
	
	loop:	beq $s0,$t7,end		# Ende, falls $s0 �ber A hinausl�uft
	lw $t1,0($s1) 		# B[i] in $t1 laden
	lw $t2,0($s2)		# C[i] in $t2 laden
	slt $t3,$t1,$t2 	# wenn $t1<$t2
	bne $t3,$zero,nimm2 	# springe zu nimm2
	
	nimm2:	sw $t2,0($s0)		# A[i]=C[i]($t2 an Speicherstelle $s0=&A[i] schreiben)
next:	addi $s0,$s0,4		# Adresse von n�chstem Element von Array A, $s0=&A[i+1]
	addi $s1,$s1,4		# Adresse von n�chstem Element von Array B, $s1=&B[i+1]
	addi $s2,$s2,4		# Adresse von n�chstem Element von Array C, $s2=&C[i+1]
	j loop			# in die n�chste Schleife springen
	
loop:	beq $s0,$t7,end		# Ende, falls $s0 �ber A hinausl�uft