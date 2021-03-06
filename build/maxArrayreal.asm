.data
A:	.word 0 0 0		# Array A
B:	.word 1 2 3		# Array B
C:	.word 3 2 1		# Array C
n:	.word 3			# n: Elemente im Array
.text
	la $s0, A		# Adresse von A in $s0 laden, $s0=&A[0]
	la $s1, B		# Adresse von B in $s1 laden, $s1=&B[0]
	la $s2, C		# Adresse von C in $s2 laden. $s2=&C[0]
	lw $s3, n		# n in $s3 laden
	
	sll $t3,$s3,2		# $t3=4*n	
	add $t7,$t3,$s0		# $t7: Adresse des ersten Worts nach Array A, $t7=&A[n]
loop:	beq $s0,$t7,end		# Ende, falls $s0 �ber A hinausl�uft
	lw $t1,0($s1) 		# B[i] in $t1 laden
	lw $t2,0($s2)		# C[i] in $t2 laden
	slt $t3,$t1,$t2 	# wenn $t1<$t2
	bne $t3,$zero,nimm2 	# springe zu nimm2
	sw $t1,0($s0) 		# A[i]=B[i] ($t1 an Speicherstelle $s0=&A[i] schreiben)
	j next			# �berspringe "else"
nimm2:	sw $t2,0($s0)		# A[i]=C[i]($t2 an Speicherstelle $s0=&A[i] schreiben)
next:	addi $s0,$s0,4		# Adresse von n�chstem Element von Array A, $s0=&A[i+1]
	addi $s1,$s1,4		# Adresse von n�chstem Element von Array B, $s1=&B[i+1]
	addi $s2,$s2,4		# Adresse von n�chstem Element von Array C, $s2=&C[i+1]
	j loop			# in die n�chste Schleife springen
end:				# Ende des Programms
	