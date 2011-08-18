#ifdef FP64

#pragma OPENCL EXTENSION cl_khr_fp64 : enable 
typedef double fp_t;
#define _F(x) x
#define EPSILON 0.000001

#elif defined AMDFP64

#pragma OPENCL EXTENSION cl_amd_fp64 : enable 
typedef double fp_t;
#define _F(x) x
#define EPSILON 0.000001

#else

typedef float fp_t;
#define _F(x) x##f
#define EPSILON 0.00001f

#endif

#define MAX_ITER 256

fp_t func(fp_t x, fp_t y) 
{   
    return {func}; 
}

fp_t df_dx(fp_t x, fp_t y)
{
    return {dfuncdx};
}

fp_t df_dy(fp_t x, fp_t y)
{
    return {dfuncdy};
}

fp_t solvex(fp_t start, const fp_t consty)
{
    for (int i = 0; i < MAX_ITER; ++i)
    {
        fp_t result = func(start, consty);
        
        if (result <= EPSILON && result >= -EPSILON)
        {
            return start;
        }

        fp_t d = df_dx(start, consty);
        if (d <= EPSILON && d >= -EPSILON)
        {
            return NAN;
        }
        else
        {
            start -= result / d;
        }
    }
    return NAN;
}

fp_t solvey(fp_t start, const fp_t constx)
{
    for (int i = 0; i < MAX_ITER; ++i)
    {
        fp_t result = func(constx, start);
        
        if (result <= EPSILON && result >= -EPSILON)
        {
            return start;
        }

        fp_t d = df_dy(constx, start);
        if (d <= EPSILON && d >= -EPSILON)
        {
            return NAN;
        }
        else
        {
            start -= result / d;
        }
    }
    return NAN;
}

kernel void ComputeX(
    global write_only fp_t* points,
    int unit,
    int width,
    int cx,
    int cy,
    fp_t origin_x,
    fp_t origin_y)
{
    int gx = get_global_id(0);
    int gy = get_global_id(1);

    uint write_loc = gx + gy * width;
    
    fp_t py = origin_y + (fp_t)(gy + 1 - cy) / unit;
    fp_t px = origin_x + (fp_t)(cx - gx - 1) / unit;

    points[write_loc] = solvex(px, py);
}

kernel void ComputeY(
    global write_only fp_t* points,
    int unit,
    int width,
    int cx,
    int cy,
    fp_t origin_x,
    fp_t origin_y)
{
    int gx = get_global_id(0);
    int gy = get_global_id(1);

    uint write_loc = gx + gy * width;
    
    fp_t py = origin_y + (fp_t)(gy + 1 - cy) / unit;
    fp_t px = origin_x + (fp_t)(cx - gx - 1) / unit;

    points[write_loc] = solvey(py, px);
}